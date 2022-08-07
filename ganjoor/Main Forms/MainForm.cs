using ganjoor.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;

/*
 * Version Pre 1.0 -> 1388/04/29
 * Hamid Reza Mohammadi https://ganjoor.net
 *
 * Version 1.0:
 *   Changelog (after a pre 1.0 release):   
 *      MainForm code moved to GanjoorView user control
 *      Added main toolbar, statusbar, about box
 *      Project code moved to sf.net : http://dg.ganjoor.net
 */

namespace ganjoor
{
    public partial class MainForm : Form
    {
        #region Constructor
        public MainForm()
        {
            InitializeComponent();
            tlbrSearch.BringToFront();
            ganjoorView.BringToFront();

            this.Bounds = Screen.PrimaryScreen.Bounds;
            if (Settings.Default.WindowMaximized)
                this.WindowState = FormWindowState.Maximized;
            else
                if (Settings.Default.WindowSize.Width != 0)
            {
                this.Bounds = new Rectangle(Settings.Default.WindowLocation, Properties.Settings.Default.WindowSize);
            }
            ApplyUserSettings();

        }
        #endregion

        #region ReUsable Methods
        private void ApplyUserSettings()
        {
            ganjoorView.CenteredView = (GanjoorViewMode)(Settings.Default.ViewMode) == GanjoorViewMode.Centered;
            ganjoorView.Font = Settings.Default.ViewFont;
            btnViewInSite.Visible = Settings.Default.BrowseButtonVisible;
            btnComments.Visible = Settings.Default.CommentsButtonVisible;
            sepWeb.Visible = btnViewInSite.Visible || btnComments.Visible;
            btnCopy.Visible = Settings.Default.CopyButtonVisible;
            btnPrint.Visible = Settings.Default.PrintButtonVisible;
            btnShowBeytNums.Visible = Settings.Default.ShowNumsVisible;
            sepTools.Visible = btnCopy.Visible || btnPrint.Visible || btnShowBeytNums.Visible;
            btnHome.Visible = sepHome.Visible = Settings.Default.HomeButtonVisible;
            btnRandom.Visible = Settings.Default.RandomButtonVisible;
            btnEditor.Visible = Settings.Default.EditorButtonVisible;
            btnDownload.Visible = Settings.Default.DownloadButtonVisible;
            processTextChanged = false;
            ganjoorView.ShowBeytNums = mnuShowBeytNums.Checked = btnShowBeytNums.Checked = Settings.Default.ShowBeytNums;
            processTextChanged = true;
            ganjoorView.ApplyUISettings();
            ganjoorView.Invalidate();

        }
        #endregion

        #region Form Events
        private void MainForm_Load(object sender, EventArgs e)
        {

            if (Settings.Default.CheckForUpdate)
                CheckForUpdate(false);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ganjoorView.StoreSettings();
            Properties.Settings.Default.WindowMaximized = (this.WindowState == FormWindowState.Maximized);
            Properties.Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();
            ganjoorView.StopPlayBack();

        }
        #endregion

        #region Button/Menu Commands

        private void btnHome_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowHome(true);
        }

        private void ganjoorView_OnPageChanged(string PageString, bool HasComments, bool CanBrowse, bool IsFaved, bool FavsPage, string HighlightedText, object preItem, object nextItem)
        {
            lblCurrentPage.Text = PageString.Length > 100 ? PageString.Substring(0, 100) + " ..." : PageString;
            if (HasComments)
            {
                btnNextPoem.Text = "شعر بعد";
                btnNextPoem.Enabled = ganjoorView.CanGoToNextPoem;
                btnNextPoem.Tag = null;
                btnPreviousPoem.Text = "شعر قبل";
                btnPreviousPoem.Enabled = ganjoorView.CanGoToPreviousPoem;
                btnPreviousPoem.Tag = null;
            }
            else
            {
                btnNextPoem.Text = "صفحهٔ بعد";
                btnNextPoem.Enabled = nextItem != null;
                btnNextPoem.Tag = nextItem;
                btnPreviousPoem.Text = "صفحهٔ قبل";
                btnPreviousPoem.Enabled = preItem != null;
                btnPreviousPoem.Tag = preItem;
            }
            mnuShowBeytNums.Enabled = btnShowBeytNums.Enabled = HasComments;
            btnComments.Enabled = HasComments;
            btnPrint.Enabled = btnComments.Enabled;
            btnHistoryBack.Enabled = ganjoorView.CanGoBackInHistory;
            btnViewInSite.Enabled = CanBrowse;
            processTextChanged = false;
            txtHighlight.Text = HighlightedText;
            processTextChanged = true;
            btnHighlight.Enabled = true;// HasComments;

            mnuAudioFiles.Enabled = btnPlayAudio.Enabled = mnuPlayAudio.Enabled = HasComments;
            mnuAudioStop.Enabled = false;

            mnuShowFavs.Checked = btnFavs.Checked = FavsPage;
            mnuFavUnFav.Enabled = btnFavUnFav.Enabled = HasComments;
            mnuFavUnFav.Image = btnFavUnFav.Image = IsFaved ? Properties.Resources.favorite_remove : Properties.Resources.favorite_add;
            mnuFavUnFav.Text = btnFavUnFav.Text = IsFaved ? "حذف نشانه" : "نشانه‌گذاری";
            mnuFavUnFav.Checked = btnFavUnFav.Checked = IsFaved;

            mnuNextPoem.Enabled = btnNextPoem.Enabled;
            mnuNextPoem.Text = btnNextPoem.Text;
            mnuPreviousPoem.Enabled = btnPreviousPoem.Enabled;
            mnuPreviousPoem.Text = btnPreviousPoem.Text;
            mnuComments.Enabled = btnComments.Enabled;
            mnuPrintPreview.Enabled = mnuPrint.Enabled = btnPrint.Enabled;
            mnuHistoryBack.Enabled = btnHistoryBack.Enabled;
            mnuViewInSite.Enabled = btnViewInSite.Enabled;
            mnuHighlight.Enabled = btnHighlight.Enabled;


            bool highlight = !string.IsNullOrEmpty(HighlightedText) && Properties.Settings.Default.ScrollToFavedVerse;
            if (highlight)
            {
                if (GanjoorViewer.OnlyScrollString != HighlightedText)
                {
                    processTextChanged = false;
                    txtHighlight.Text = HighlightedText;
                    btnHighlight.Checked = highlight;
                    processTextChanged = true;
                    iLastFoundItems = ganjoorView.HighlightText(HighlightedText);
                    iLastHighlightedFoundItem = 0;
                    btnScrollToNext.Visible = iLastFoundItems > 1;
                    lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", iLastFoundItems);
                }
                else
                {
                    processTextChanged = false;
                    txtHighlight.Text = "";
                    btnHighlight.Checked = false;
                    processTextChanged = true;
                    iLastFoundItems = ganjoorView.HighlightText(HighlightedText);
                    iLastHighlightedFoundItem = 0;
                    btnScrollToNext.Visible = iLastFoundItems > 1;
                    highlight = false;
                }
            }
            else
                btnHighlight.Checked = false;
            lblFoundItemCount.Visible = highlight;


            txtHighlight.Focus();



        }

        private void btnPreviousPoem_Click(object sender, EventArgs e)
        {
            if (btnPreviousPoem.Tag == null)
            {
                ganjoorView.PreviousPoem();
            }
            else
            {
                ganjoorView.ProcessPagingTag(btnPreviousPoem.Tag);
            }
        }

        private void btnNextPoem_Click(object sender, EventArgs e)
        {
            if (btnNextPoem.Tag == null)
            {
                ganjoorView.NextPoem();
            }
            else
            {
                ganjoorView.ProcessPagingTag(btnNextPoem.Tag);
            }
        }

        private void btnViewInSite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ganjoorView.CurrentPageGanjoorUrl))
            {
                MessageBox.Show("امکان نمایش صفحهٔ معادل در سایت گنجور وجود ندارد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
            else
                try
                {
                    System.Diagnostics.Process.Start(ganjoorView.CurrentPageGanjoorUrl);
                }
                catch
                {
                }
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ganjoorView.CurrentPageGanjoorUrl))
            {
                MessageBox.Show("امکان نمایش صفحهٔ معادل در سایت گنجور وجود ندارد.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
            }
            else
                try
                {
                    System.Diagnostics.Process.Start(ganjoorView.CurrentPoemCommentsUrl);
                }
                catch
                {
                }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dlg = new PrintDialog())
            {
                using (dlg.Document = new PrintDocument())
                {
                    if (dlg.ShowDialog(this) == DialogResult.OK)
                    {
                        ganjoorView.Print(dlg.Document);
                    }
                }
            }
        }

        private void mnuPrintPreview_Click(object sender, EventArgs e)
        {
            using (PrintPreviewDialog dlg = new PrintPreviewDialog())
            {
                dlg.ShowIcon = false;
                dlg.UseAntiAlias = true;
                using (PrintDocument Document = ganjoorView.PrepareForPrintPreview())
                {
                    dlg.Document = Document;
                    dlg.ShowDialog(this);
                }
            }
        }

        private void btnHistoryBack_Click(object sender, EventArgs e)
        {
            ganjoorView.GoBackInHistory();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            using (AboutForm dlg = new AboutForm())
            {
                dlg.ShowDialog(this);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (Search dlg = new Search())
            {
                dlg.Poets = ganjoorView.Poets;
                dlg.PoetOrder = ganjoorView.GetPoetOrder(Properties.Settings.Default.LastSearchPoetID);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    Properties.Settings.Default.LastSearchPoetID = ganjoorView.GetPoetID(dlg.PoetOrder);
                    Properties.Settings.Default.LastSearchPhrase = dlg.Phrase;
                    Properties.Settings.Default.LastSearchType = dlg.SearchType;
                    Properties.Settings.Default.LastSearchLocationType = dlg.SearchLocationType;
                    Properties.Settings.Default.SearchPageItems = dlg.ItemsInPage;
                    Properties.Settings.Default.Save();

                    ganjoorView.ShowSearchResults(dlg.Phrase, 0, dlg.ItemsInPage, ganjoorView.GetPoetID(dlg.PoetOrder), searchType: dlg.SearchType, searchLocationType: dlg.SearchLocationType);
                }
            }
        }

        private void btnCopyText_Click(object sender, EventArgs e)
        {
            ganjoorView.CopyText(ModifierKeys == Keys.Shift);
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            using (Options dlg = new Options())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    ApplyUserSettings();
                }
            }
        }


        private void btnHighlight_CheckedChanged(object sender, EventArgs e)
        {
            if (tlbrSearch.Visible = btnHighlight.Checked)
            {
                lblFoundItemCount.Visible = !string.IsNullOrEmpty(txtHighlight.Text);
                if (!string.IsNullOrEmpty(txtHighlight.Text))
                    txtHighlight_TextChanged(sender, e);
                txtHighlight.Focus();
            }
            else
            {
                processTextChanged = false;
                txtHighlight.Text = "";
                ganjoorView.HighlightText(string.Empty);
                iLastFoundItems = 0;
                lblFoundItemCount.Visible = false;
                iLastHighlightedFoundItem = 0;
                btnScrollToNext.Visible = false;
                processTextChanged = true;
                ganjoorView.Focus();
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuHighlight_Click(object sender, EventArgs e)
        {
            btnHighlight.Checked = !btnHighlight.Checked;
        }

        #region Find In Page
        private bool processTextChanged = true;
        private int iLastFoundItems = 0;
        private void txtHighlight_TextChanged(object sender, EventArgs e)
        {
            if (processTextChanged)
            {
                iLastFoundItems = ganjoorView.HighlightText(GPersianTextSync.Sync(txtHighlight.Text));
                iLastHighlightedFoundItem = 0;
                if (lblFoundItemCount.Visible = !string.IsNullOrEmpty(txtHighlight.Text))
                    lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", iLastFoundItems);
                btnScrollToNext.Visible = iLastFoundItems > 1;

            }
        }
        private int iLastHighlightedFoundItem = 0;
        private void btnScrollToNext_Click(object sender, EventArgs e)
        {
            iLastHighlightedFoundItem++;
            ganjoorView.HighlightText(GPersianTextSync.Sync(txtHighlight.Text), iLastHighlightedFoundItem);
            btnScrollToNext.Visible = (iLastHighlightedFoundItem + 1 < iLastFoundItems);
        }
        private void txtHighlight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if (btnScrollToNext.Visible)
                    btnScrollToNext_Click(sender, new EventArgs());
        }
        #endregion


        private void btnFavUnFav_Click(object sender, EventArgs e)
        {
            bool result = ganjoorView.ToggleFav();
            mnuFavUnFav.Image = btnFavUnFav.Image = result ? Properties.Resources.favorite_remove : Properties.Resources.favorite_add;
            mnuFavUnFav.Text = btnFavUnFav.Text = result ? "حذف نشانه" : "نشانه‌گذاری";
            mnuFavUnFav.Checked = btnFavUnFav.Checked = result;
        }

        private void btnFavs_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowFavs(0, Settings.Default.FavItemsInPage);
        }

        private void btnShowBeytNums_CheckedChanged(object sender, EventArgs e)
        {
            if (processTextChanged)
            {
                Settings.Default.ShowBeytNums = !Settings.Default.ShowBeytNums;
                Settings.Default.Save();
                ganjoorView.SetShowBeytNums(Settings.Default.ShowBeytNums);
            }
        }

        private void mnuShowBeytNums_Click(object sender, EventArgs e)
        {
            mnuShowBeytNums.Checked = btnShowBeytNums.Checked = !btnShowBeytNums.Checked;
        }


        private void btnCheckForUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdate(true);
        }

        private void CheckForUpdate(bool Prompt)
        {
            try
            {
                WebRequest req = WebRequest.Create("http://dg.ganjoor.net/version.xml");
                GConnectionManager.ConfigureProxy(ref req);
                using (WebResponse response = req.GetResponse())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(reader.ReadToEnd());
                            int MyVersionMajor = Assembly.GetExecutingAssembly().GetName().Version.Major;
                            int MyVersionMinor = Assembly.GetExecutingAssembly().GetName().Version.Minor;
                            int VersionMajor = 0;
                            int VersionMinor = 0;
                            string updateUrl = string.Empty;
                            XmlNode versionNode = doc.GetElementsByTagName("Version")[0];
                            foreach (XmlNode Node in versionNode.ChildNodes)
                            {
                                if (Node.Name == "Major")
                                    VersionMajor = Convert.ToInt32(Node.InnerText);
                                else
                                    if (Node.Name == "Minor")
                                    VersionMinor = Convert.ToInt32(Node.InnerText);
                                else
                                        if (Node.Name == "UpdateUrl")
                                {
                                    if (string.IsNullOrEmpty(updateUrl))
                                        updateUrl = Node.InnerText;
                                }
                                else
                                            if (Node.Name == "UpdateUrl162Plus")
                                    updateUrl = Node.InnerText;

                            }
                            if (VersionMajor == MyVersionMajor && VersionMinor == MyVersionMinor)
                            {
                                if (Prompt)
                                {
                                    MessageBox.Show("شما آخرین ویرایش گنجور رومیزی را در اختیار دارید.", "تبریک", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                                }
                            }
                            else
                            {
                                if (
                                MessageBox.Show("ویرایش جدیدتر " + VersionMajor.ToString() + "." + VersionMinor.ToString() + " از نرم‌افزار ارائه شده است. صفحهٔ دریافت باز شود؟ ", "ویرایش جدید", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                                   ==
                                   DialogResult.Yes
                                    )
                                {
                                    System.Diagnostics.Process.Start(updateUrl);
                                    if (!Prompt)//check for new gdbs
                                        return;
                                }
                            }
                        }
                    }
                }

                if (!Prompt)//check for new gdbs
                {
                    string strException;
                    List<GDBInfo> Lst = GDBListProcessor.RetrieveList("http://i.ganjoor.net/android/androidgdbs.xml", out strException);
                    if (Lst != null && string.IsNullOrEmpty(strException))
                    {
                        List<GDBInfo> finalList = new List<GDBInfo>();
                        DbBrowser db = new DbBrowser();
                        foreach (GDBInfo gdb in Lst)
                            if (
                                !db.IsInGDBIgnoreList(gdb.CatID)
                                &&
                                db.GetCategory(gdb.CatID) == null
                               )
                            {
                                finalList.Add(gdb);
                            }
                        if (finalList.Count > 0)
                        {
                            using (NewGDBFound dlg = new NewGDBFound(finalList))
                            {
                                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                    using (DownloadingGdbList dwnDlg = new DownloadingGdbList(dlg.dwnldList))
                                        if (dwnDlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                                            foreach (string DownloadedFile in dwnDlg.DownloadedFiles)
                                            {
                                                ImportGdb(DownloadedFile);
                                                if (Settings.Default.DeleteDownloadedFiles)
                                                    File.Delete(DownloadedFile);
                                            }
                                foreach (int CatID in dlg.IgnoreList)
                                    db.AddToGDBIgnoreList(CatID);
                            }
                        }
                    }

                }
            }
            catch (Exception exp)
            {
                if (Prompt)
                {
                    MessageBox.Show(exp.Message, "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowRandomPoem();
        }

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            ganjoorView.Visible = false;
            try
            {
                ganjoorView.Font = new Font(ganjoorView.Font.Name, Math.Min(144.0f, ganjoorView.Font.Size * 1.1f), ganjoorView.Font.Style);
                Settings.Default.ViewFont = ganjoorView.Font;
            }
            catch
            {
            }
            ganjoorView.Visible = true;
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            ganjoorView.Visible = false;
            try
            {
                ganjoorView.Font = new Font(ganjoorView.Font.Name, Math.Max(4.0f, ganjoorView.Font.Size * 0.9f), ganjoorView.Font.Style);
                Settings.Default.ViewFont = ganjoorView.Font;
            }
            catch
            {
            }

            ganjoorView.Visible = true;
        }

        private void mnuAdd_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "All Supported Packages|*.gdb;*.s3db;*.zip|GDB Files(*.gdb)|*.gdb|Poem SQLite databases(*.s3db)|*.s3db|Zipped GDB Files(*.zip)|*.zip";
                dlg.Multiselect = true;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    foreach (string FileName in dlg.FileNames)
                    {
                        ImportGdb(FileName);
                    }
                }
            }
        }

        private void mnuDel_Click(object sender, EventArgs e)
        {

            if (ganjoorView.CurrentPoet == "همه")
            {
                MessageBox.Show("لطفاً وارد آثار شاعر مورد نظرتان شوید.", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                return;
            }

            if (
                 MessageBox.Show(
                 String.Format(
                        "آیا از حذف «{0}» و تمام آثار او اطمینان دارید؟", ganjoorView.CurrentPoet
                        ), "تأییدیه", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                 ==
                 DialogResult.Yes
                 )
                if (!ganjoorView.DeletePoet())
                    MessageBox.Show(string.Format("خطا رخ داد. {0}", ganjoorView.LastError), "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
        }


        private void ImportGdb(string FileName)
        {
            if (Path.GetExtension(FileName).Equals(".zip", StringComparison.InvariantCultureIgnoreCase))
            {
                using (ZipStorer zip = ZipStorer.Open(FileName, FileAccess.Read))
                {
                    List<ZipStorer.ZipFileEntry> dir = zip.ReadCentralDir();
                    foreach (ZipStorer.ZipFileEntry entry in dir)
                    {
                        string gdbFileName = Path.GetFileName(entry.FilenameInZip);
                        if (Path.GetExtension(gdbFileName).Equals(".gdb") || Path.GetExtension(gdbFileName).Equals(".s3db"))
                        {
                            string ganjoorPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ganjoor");
                            if (!Directory.Exists(ganjoorPath))
                                Directory.CreateDirectory(ganjoorPath);
                            string gdbExtractPath = Path.Combine(ganjoorPath, gdbFileName);
                            if (zip.ExtractFile(entry, gdbExtractPath))
                            {
                                ganjoorView.ImportDb(gdbExtractPath);
                                File.Delete(gdbExtractPath);
                            }
                        }
                    }
                }
            }
            else
                ganjoorView.ImportDb(FileName);
        }

        private void mnuExportFavs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "*.gdb|*.gdb";
                dlg.FileName = "export.gdb";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    ganjoorView.ExportFavs(dlg.FileName);
            }
        }

        private void mnuImportFavs_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "*.gdb|*.gdb|*.*|*.*";
                dlg.FileName = "export.gdb";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    ganjoorView.ImportMixFavs(dlg.FileName);
            }
        }

        private void btnEditor_Click(object sender, EventArgs e)
        {
            ganjoorView.StoreSettings();
            this.Hide();
            using (Editor dlg = new Editor())
                dlg.ShowDialog(this);
            this.Show();
        }

        private void mnuAddUnsafe_Click(object sender, EventArgs e)
        {
            MessageBox.Show("در این روش اضافه کردن اشعار آزمونهای جلوگیری از خطا انجام نمی‌شود. به همین دلیل فرایند اضافه شدن شعرها سریع‌تر است. در صورت برخورد به خطا در هنگام استفاده از این روش، از فرمان استاندارد اضافه کردن اشعار استفاده کنید.", "هشدار", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "*.gdb|*.gdb|*.s3db|*.s3db";
                dlg.FileName = "new.gdb";
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    ganjoorView.ImportDbUnsafe(dlg.FileName);
            }
        }

        #region AutoScroll fix found at http://www.devnewsgroups.net/group/microsoft.public.dotnet.framework.windowsforms/topic22846.aspx
        private Point thumbPos = new Point();
        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            thumbPos = ganjoorView.AutoScrollPosition;
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            thumbPos.X *= -1;
            thumbPos.Y *= -1;
            ganjoorView.AutoScrollPosition = thumbPos;
        }
        #endregion


        private void btnChangeLog_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://dg.ganjoor.net/changelog/");
        }

        private void btnDownload_VisibleChanged(object sender, EventArgs e)
        {
            sepDownloadOptions.Visible = btnDownload.Visible;
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            using (GDBDownloadWizard wiz = new GDBDownloadWizard())
            {
                wiz.ShowDialog(this);
                if (wiz.AnythingInstalled)
                    ganjoorView.ShowHome(true);
            }
        }

        private void ganjoorView_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!btnHighlight.Checked)
            {
                if (GPersianTextSync.PersianLetters.IndexOf(e.KeyChar) != -1)
                {
                    btnHighlight.Checked = true;
                    txtHighlight.Text = "" + e.KeyChar;
                    txtHighlight.SelectionStart = txtHighlight.Text.Length;
                }
            }
        }


        #region Audio Playback

        private void btnPlayAudio_Click(object sender, EventArgs e)
        {
            if (ganjoorView.IsPlaying)
            {
                ganjoorView.Pause();
                mnuPlayAudio.Text = btnPlayAudio.Text = "ادامه";
                mnuPlayAudio.Image = btnPlayAudio.Image = Properties.Resources.play;
                return;
            }
            if (ganjoorView.IsInPauseState)
            {
                ganjoorView.Resume();
                mnuPlayAudio.Text = btnPlayAudio.Text = "توقف";
                mnuPlayAudio.Image = btnPlayAudio.Image = Properties.Resources.pause;
                return;

            }
            PoemAudio[] poemAudioFiles = this.ganjoorView.PoemAudioFiles;
            if (poemAudioFiles.Length == 0)
            {
                ManagePoemAudioFiles();
            }
            else
            {
                PoemAudio poemAudio = poemAudioFiles[0];
                if (poemAudioFiles.Length > 1)
                {
                    ItemSelector dlg = new ItemSelector("گزینش خوانش ...", poemAudioFiles, poemAudio);
                    if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                    {
                        poemAudio = dlg.SelectedItem as PoemAudio;
                    }
                    else
                        return;
                }
                ganjoorView.Play(poemAudio);
            }
        }

        private void ganjoorView_PlaybackStarted(object sender, EventArgs e)
        {
            mnuPlayAudio.Text = btnPlayAudio.Text = "توقف";
            mnuPlayAudio.Image = btnPlayAudio.Image = Properties.Resources.pause;
            mnuAudioStop.Enabled = true;

        }

        private void ganjoorView_PlaybackStopped(object sender, EventArgs e)
        {
            mnuPlayAudio.Text = btnPlayAudio.Text = "خوانش";
            mnuPlayAudio.Image = btnPlayAudio.Image = Properties.Resources.sound;
            mnuAudioStop.Enabled = false;
        }

        private void mnuAudioStop_Click(object sender, EventArgs e)
        {
            ganjoorView.StopPlayBack(true);
        }


        private void ManagePoemAudioFiles()
        {
            using AudioFiles dlg = new(ganjoorView.CurrentPoemId);
            dlg.ShowDialog();
        }

        private void mnuAudioFiles_Click(object sender, EventArgs e)
        {
            ManagePoemAudioFiles();
        }



        #endregion

        private void mnuAllAudioFile_Click(object sender, EventArgs e)
        {
            using NarratedPoems dlg = new();
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ganjoorView.ShowPoem(dlg.SelectedPoem, true);
            }
        }
        #endregion
    }
}
