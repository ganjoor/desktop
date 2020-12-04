using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ganjoor.Audio_Support
{
    public partial class GLogin : Form
    {
        public GLogin()
        {
            InitializeComponent();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            Process.Start("https://gaudiopanel.ganjoor.net");
            Cursor = Cursors.Default;
        }

        private async void btnLogin_ClickAsync(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Enabled = false;
            Application.DoEvents();

            DialogResult = DialogResult.None;
            LoginViewModel model = new LoginViewModel()
            {
                Username = txtEmail.Text,
                Password = txtPassword.Text,
                ClientAppName = "Desktop Ganjoor",
                Language = "fa-IR"
            };

            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                var loginUrl = $"{Properties.Settings.Default.GanjoorServiceUrl}/api/users/login";
                var response = await httpClient.PostAsync(loginUrl, stringContent);
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Cursor = Cursors.Default;
                    Enabled = true;
                    MessageBox.Show(response.ToString());
                    return;
                }
                response.EnsureSuccessStatusCode();

                var result = JObject.Parse(await response.Content.ReadAsStringAsync());
                Properties.Settings.Default.MuseumToken = result["token"].ToString();
                Properties.Settings.Default.SessionId = Guid.Parse(result["sessionId"].ToString());
                Properties.Settings.Default.Save();
            }

            Enabled = true;
            Cursor = Cursors.Default;
            DialogResult = DialogResult.OK;

        }
    }
}
