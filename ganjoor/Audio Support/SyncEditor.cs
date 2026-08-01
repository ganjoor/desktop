
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    /// <summary>
    /// ویرایشگر اطلاعات همگام‌سازی صوت با شعر.
    /// این فرم برای اصلاح همگام‌سازی‌ای که پیشتر انجام شده به کار می‌رود:
    /// کاربر یک مصرع را از فهرست انتخاب می‌کند، با گوش دادن به صدا (یا پیمایش نوار پخش)
    /// لحظهٔ درست آغاز آن مصرع را پیدا می‌کند و آن را برای همان مصرع ثبت می‌کند،
    /// دقیقا مانند یک ویرایشگر زیرنویس.
    /// </summary>
    public partial class SyncEditor : Form
    {
        public SyncEditor(DbBrowser dbBrowser, PoemAudio poemAudio)
        {
            InitializeComponent();
            _PoemAudio = poemAudio;
            _DbBrowser = dbBrowser;
            _PoemAudioPlayer = new PoemAudioPlayer();
            _PoemAudioPlayer.PlaybackStarted += new EventHandler(_PoemAudioPlayer_PlaybackStarted);
            _PoemAudioPlayer.PlaybackStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(_PoemAudioPlayer_PlaybackStopped);
            _PoemVerses = _DbBrowser.GetVerses(poemAudio.PoemId).ToArray();

            _VerseMilisecPositions = new List<PoemAudio.SyncInfo>();
            if (poemAudio.SyncArray != null)
            {
                foreach (var syncInfo in poemAudio.SyncArray)
                {
                    var info = syncInfo;
                    var verse = _PoemVerses.Where(v => v._Order == (info.VerseOrder + 1)).FirstOrDefault();
                    if (verse != null)
                    {
                        info.VerseText = verse._Text;
                    }
                    _VerseMilisecPositions.Add(info);
                }
            }
        }

        private DbBrowser _DbBrowser;
        private PoemAudio _PoemAudio;
        private GanjoorVerse[] _PoemVerses;
        private List<PoemAudio.SyncInfo> _VerseMilisecPositions;
        private bool _Modified = false;

        private const int NudgeStepMiliseconds = 100;

        #region Load / list population

        private void SyncEditor_Load(object sender, EventArgs e)
        {
            RefreshList();

            if (_VerseMilisecPositions.Count == 0)
            {
                MessageBox.Show("این فایل صوتی هنوز همگام‌سازی نشده است. ابتدا باید همگام‌سازی اولیه انجام شود.");
            }

            // Load the audio file and pause immediately so the trackbar/list can be used
            // for scrubbing to find exact positions even before pressing play.
            if (_PoemAudioPlayer.BeginPlayback(_PoemAudio))
            {
                _PoemAudioPlayer.PausePlayBack();
                trackBar.Maximum = Math.Max(1, _PoemAudioPlayer.TotalTimeInMiliseconds);
                trackBar.Enabled = true;
                btnPlayPause.Text = "پخش صدا";
                btnPlayPause.Image = Properties.Resources.play;
                lblTime.Text = TimeSpan.FromMilliseconds(_PoemAudioPlayer.PositionInMiliseconds).ToString();
            }
            else
            {
                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
            }

            if (lvVerses.Items.Count > 0)
            {
                lvVerses.Items[0].Selected = true;
                lvVerses.Items[0].Focused = true;
            }

            EnableButtons();
        }

        private void RefreshList()
        {
            int selectedIndex = lvVerses.SelectedIndices.Count > 0 ? (int)lvVerses.SelectedItems[0].Tag : -1;

            lvVerses.BeginUpdate();
            lvVerses.Items.Clear();
            for (int i = 0; i < _VerseMilisecPositions.Count; i++)
            {
                var syncInfo = _VerseMilisecPositions[i];
                var item = new ListViewItem((i + 1).ToString());
                item.SubItems.Add(FormatTime(syncInfo.AudioMiliseconds));
                item.SubItems.Add(syncInfo.VerseText);
                item.Tag = i;
                lvVerses.Items.Add(item);
            }
            lvVerses.EndUpdate();

            if (selectedIndex >= 0 && selectedIndex < lvVerses.Items.Count)
            {
                lvVerses.Items[selectedIndex].Selected = true;
                lvVerses.Items[selectedIndex].Focused = true;
                lvVerses.Items[selectedIndex].EnsureVisible();
            }
        }

        private static string FormatTime(int miliseconds)
        {
            if (miliseconds < 0)
                miliseconds = 0;
            return TimeSpan.FromMilliseconds(miliseconds).ToString(@"hh\:mm\:ss\.fff");
        }

        private void EnableButtons()
        {
            bool hasSelection = lvVerses.SelectedIndices.Count > 0;
            bool audioReady = trackBar.Enabled;
            btnSetStart.Enabled = hasSelection && audioReady;
            btnEarlier.Enabled = hasSelection;
            btnLater.Enabled = hasSelection;
            btnSave.Enabled = _Modified;
        }

        #endregion

        #region Selection

        private bool _UpdatingSelectionProgrammatically = false;

        private void lvVerses_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();

            if (lvVerses.SelectedItems.Count == 0)
            {
                lblSelectedInfo.Text = "";
                return;
            }

            int index = (int)lvVerses.SelectedItems[0].Tag;
            var syncInfo = _VerseMilisecPositions[index];
            lblSelectedInfo.Text = String.Format("مصرع انتخاب شده: {0} ({1})", syncInfo.VerseText, FormatTime(syncInfo.AudioMiliseconds));

            // Jump playback to the selected verse's current start position so the user can
            // immediately hear the context, unless this selection change was made
            // programmatically while following playback.
            if (!_UpdatingSelectionProgrammatically && !_PoemAudioPlayer.IsPlaying)
            {
                SeekTo(syncInfo.AudioMiliseconds);
            }
        }

        private void lvVerses_DoubleClick(object sender, EventArgs e)
        {
            if (lvVerses.SelectedItems.Count == 0)
                return;
            int index = (int)lvVerses.SelectedItems[0].Tag;
            SeekTo(_VerseMilisecPositions[index].AudioMiliseconds);
        }

        #endregion

        #region Audio Playback

        private PoemAudioPlayer _PoemAudioPlayer;
        private bool _TrackbarValueSetting = false;

        private void SeekTo(int miliseconds)
        {
            if (!trackBar.Enabled)
                return;
            miliseconds = Math.Max(0, Math.Min(miliseconds, trackBar.Maximum));
            _PoemAudioPlayer.PositionInMiliseconds = miliseconds;
            _TrackbarValueSetting = true;
            trackBar.Value = miliseconds;
            _TrackbarValueSetting = false;
            lblTime.Text = TimeSpan.FromMilliseconds(miliseconds).ToString();
        }

        private void _PoemAudioPlayer_PlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs e)
        {
            // we want to be always on the GUI thread and be able to change GUI components
            Debug.Assert(!this.InvokeRequired, "PlaybackStopped on wrong thread");
            if (e.Exception != null)
            {
                MessageBox.Show(String.Format("Playback Stopped due to an error {0}", e.Exception.Message));
            }
            timer.Stop();
            btnPlayPause.Text = "پخش صدا";
            btnPlayPause.Image = Properties.Resources.play;

            // Play reached the end of the file (CleanUp already ran). Re-arm playback in
            // paused state at position 0 so the trackbar/list stay usable for scrubbing.
            if (_PoemAudioPlayer.BeginPlayback(_PoemAudio))
            {
                _PoemAudioPlayer.PausePlayBack();
                trackBar.Enabled = true;
                SeekTo(0);
            }
            else
            {
                trackBar.Enabled = false;
            }
            EnableButtons();
        }

        private void _PoemAudioPlayer_PlaybackStarted(object sender, EventArgs e)
        {
        }

        #endregion

        private void btnPlayPause_Click(object sender, EventArgs e)
        {
            if (_PoemAudioPlayer.IsPlaying)
            {
                _PoemAudioPlayer.PausePlayBack();
                btnPlayPause.Text = "ادامۀ پخش";
                btnPlayPause.Image = Properties.Resources.play;
                timer.Stop();
                return;
            }
            if (_PoemAudioPlayer.IsInPauseState)
            {
                btnPlayPause.Text = "توقف";
                btnPlayPause.Image = Properties.Resources.pause;
                _PoemAudioPlayer.ResumePlayBack();
                timer.Start();
                return;
            }
            if (!_PoemAudioPlayer.BeginPlayback(_PoemAudio))
            {
                MessageBox.Show("خطایی در پخش فایل صوتی رخ داد. لطفا چک کنید فایل در مسیر تعیین شده قرار داشته باشد.");
                return;
            }

            btnPlayPause.Text = "توقف";
            btnPlayPause.Image = Properties.Resources.pause;
            trackBar.Maximum = Math.Max(1, _PoemAudioPlayer.TotalTimeInMiliseconds);
            trackBar.Enabled = true;
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int nPositionInMiliseconds = _PoemAudioPlayer.PositionInMiliseconds;
            _TrackbarValueSetting = true;
            trackBar.Value = Math.Min(nPositionInMiliseconds, trackBar.Maximum);
            _TrackbarValueSetting = false;

            lblTime.Text = TimeSpan.FromMilliseconds(nPositionInMiliseconds).ToString();

            FollowPlaybackSelection(nPositionInMiliseconds);
        }

        /// <summary>
        /// در حین پخش، مصرعی که هم اکنون در حال پخش شدن است در فهرست انتخاب می‌شود
        /// تا کاربر بداند لحظهٔ جاری مربوط به کدام مصرع است.
        /// </summary>
        private void FollowPlaybackSelection(int positionInMiliseconds)
        {
            int matchIndex = -1;
            for (int i = 0; i < _VerseMilisecPositions.Count; i++)
            {
                if (_VerseMilisecPositions[i].AudioMiliseconds <= positionInMiliseconds)
                {
                    matchIndex = i;
                }
                else
                {
                    break;
                }
            }

            if (matchIndex < 0)
                return;

            bool alreadySelected = lvVerses.SelectedIndices.Count > 0 && (int)lvVerses.SelectedItems[0].Tag == matchIndex;
            if (!alreadySelected)
            {
                _UpdatingSelectionProgrammatically = true;
                foreach (ListViewItem selected in lvVerses.SelectedItems.Cast<ListViewItem>().ToArray())
                {
                    selected.Selected = false;
                }
                lvVerses.Items[matchIndex].Selected = true;
                lvVerses.Items[matchIndex].EnsureVisible();
                _UpdatingSelectionProgrammatically = false;
            }
        }

        private void SyncEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_Modified)
            {
                if (MessageBox.Show("تغییرات ذخیره نشده‌اند. فرم را می‌بندید؟", "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }
            timer.Stop();
            if (_PoemAudioPlayer.IsPlaying || _PoemAudioPlayer.IsInPauseState)
            {
                _PoemAudioPlayer.StopPlayBack();
            }
            _PoemAudioPlayer.CleanUp();
        }

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            if (_TrackbarValueSetting)
                return;
            _PoemAudioPlayer.PositionInMiliseconds = trackBar.Value;
            lblTime.Text = TimeSpan.FromMilliseconds(trackBar.Value).ToString();
        }

        /// <summary>
        /// موقعیت جاری پخش را به عنوان آغاز مصرع انتخاب‌شده در فهرست ثبت می‌کند.
        /// </summary>
        private void btnSetStart_Click(object sender, EventArgs e)
        {
            if (lvVerses.SelectedItems.Count == 0)
                return;

            int index = (int)lvVerses.SelectedItems[0].Tag;
            int newPosition = _PoemAudioPlayer.PositionInMiliseconds;

            ApplyNewStart(index, newPosition);
        }

        private void btnEarlier_Click(object sender, EventArgs e)
        {
            NudgeSelected(-NudgeStepMiliseconds);
        }

        private void btnLater_Click(object sender, EventArgs e)
        {
            NudgeSelected(NudgeStepMiliseconds);
        }

        private void NudgeSelected(int deltaMiliseconds)
        {
            if (lvVerses.SelectedItems.Count == 0)
                return;

            int index = (int)lvVerses.SelectedItems[0].Tag;
            int newPosition = _VerseMilisecPositions[index].AudioMiliseconds + deltaMiliseconds;
            ApplyNewStart(index, newPosition, warnOnReorder: false);
            SeekTo(_VerseMilisecPositions[index].AudioMiliseconds);
        }

        private void ApplyNewStart(int index, int newPositionInMiliseconds, bool warnOnReorder = true)
        {
            int maxPosition = trackBar.Enabled ? trackBar.Maximum : int.MaxValue;
            newPositionInMiliseconds = Math.Max(0, Math.Min(newPositionInMiliseconds, maxPosition));

            if (warnOnReorder)
            {
                bool beforePrevious = index > 0 && newPositionInMiliseconds < _VerseMilisecPositions[index - 1].AudioMiliseconds;
                bool afterNext = index < _VerseMilisecPositions.Count - 1 && newPositionInMiliseconds > _VerseMilisecPositions[index + 1].AudioMiliseconds;
                if (beforePrevious || afterNext)
                {
                    if (MessageBox.Show(
                        "زمان جدید با ترتیب مصرعهای مجاور همخوانی ندارد. آیا مطمئن هستید؟",
                        "تأییدیه",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2,
                        MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            var syncInfo = _VerseMilisecPositions[index];
            syncInfo.AudioMiliseconds = newPositionInMiliseconds;
            _VerseMilisecPositions[index] = syncInfo;

            _Modified = true;
            RefreshList();
            EnableButtons();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _DbBrowser.SavePoemSync(_PoemAudio, _VerseMilisecPositions.ToArray(), true);
            _PoemAudio.SyncArray = _VerseMilisecPositions.ToArray();
            _Modified = false;
            EnableButtons();
            MessageBox.Show("تغییرات با موفقیت ذخیره شد.", "اعلان", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading);
        }

        private void SyncEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.P)
            {
                btnPlayPause_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Left)
            {
                btnEarlier_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Control && e.KeyCode == Keys.Right)
            {
                btnLater_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter && btnSetStart.Enabled)
            {
                btnSetStart_Click(sender, EventArgs.Empty);
                e.Handled = true;
            }
        }
    }
}
