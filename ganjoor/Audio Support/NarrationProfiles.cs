using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ganjoor.Audio_Support
{
    public partial class NarrationProfiles : Form
    {
        public NarrationProfiles()
        {
            InitializeComponent();
        }

        private async Task LoadProfiles()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.MuseumToken);

                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                HttpResponseMessage response = await httpClient.GetAsync($"{Properties.Settings.Default.GanjoorServiceUrl}/api/audio/profile");
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

        private async void NarrationProfiles_Load(object sender, EventArgs e)
        {
            await LoadProfiles();
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using(NarrationProfile dlg = new NarrationProfile())
            {
                if(dlg.ShowDialog(this) == DialogResult.OK)
                {
                    await LoadProfiles();
                }
            }
        }
    }
}
