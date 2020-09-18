using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class UploadedNarrations : Form
    {
        public UploadedNarrations()
        {
            InitializeComponent();
            cmbStatus.SelectedIndex = 1;
        }

        private async Task LoadNarrations()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                HttpResponseMessage response = await httpClient.GetAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio?status={cmbStatus.SelectedIndex - 1}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(response.ToString());
                    return;
                }

                grd.DataSource = await response.Content.ReadAsAsync<object>();

            }
            Cursor = Cursors.Default;
        }
        
        private async void UploadedNarrations_LoadAsync(object sender, EventArgs e)
        {
            await LoadNarrations();
        }

        private async void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadNarrations();
        }
    }
}
