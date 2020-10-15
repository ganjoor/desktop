using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class NarrationProfile : Form
    {
        public NarrationProfile(UserNarrationProfileViewModel editingRecord = null)
        {
            InitializeComponent();
            _editingRecord = editingRecord;
            if(_editingRecord != null)
            {
                txtName.Text = _editingRecord.ArtistName;
                txtFileSuffixWithoutDash.Text = _editingRecord.FileSuffixWithoutDash;
                txtAudioArtistUrl.Text = _editingRecord.ArtistUrl;
                txtAudioSrc.Text = _editingRecord.AudioSrc;
                txtAudioArtistUrl.Text = _editingRecord.AudioSrcUrl;
                chkDefault.Checked = _editingRecord.IsDefault;
                
            }
        }

        UserNarrationProfileViewModel _editingRecord;

        private async void btnOK_ClickAsync(object sender, EventArgs e)
        {
                        

            if(_editingRecord == null) //جدید
            {
                UserNarrationProfileViewModel newRecord = new UserNarrationProfileViewModel()
                {
                    ArtistName = txtName.Text,
                    FileSuffixWithoutDash = txtFileSuffixWithoutDash.Text,
                    ArtistUrl = txtAudioArtistUrl.Text,
                    AudioSrc = txtAudioSrc.Text,
                    AudioSrcUrl = txtAudioSrcUrl.Text,
                    IsDefault = chkDefault.Checked

                };

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    HttpResponseMessage response = await httpClient.PostAsync
                        (
                        $"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/profile",
                        newRecord,
                        new JsonMediaTypeFormatter()
                        );
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        DialogResult = DialogResult.None;
                        return;
                    }

                }
                Cursor = Cursors.Default;
            }
            else
            {
                _editingRecord.ArtistName = txtName.Text;
                _editingRecord.FileSuffixWithoutDash = txtFileSuffixWithoutDash.Text;
                _editingRecord.ArtistUrl = txtAudioArtistUrl.Text;
                _editingRecord.AudioSrc = txtAudioSrc.Text;
                _editingRecord.AudioSrcUrl = txtAudioArtistUrl.Text;
                _editingRecord.IsDefault = chkDefault.Checked;

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();

                    HttpResponseMessage response = await httpClient.PutAsync
                        (
                        $"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/profile",
                        _editingRecord,
                        new JsonMediaTypeFormatter()
                        );
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(response.ToString());
                        DialogResult = DialogResult.None;
                        return;
                    }

                }
                Cursor = Cursors.Default;
            }


        }
    }
}
