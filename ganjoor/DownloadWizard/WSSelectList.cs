using System;
using System.Collections.Generic;
using System.Text;
using ganjoor.Properties;


namespace ganjoor
{

    partial class WSSelectList : WizardStage
    {

        public WSSelectList() : base() { InitializeComponent(); }
        public override void OnBeforeActivate()
        {
            InitiateList();
        }

        private void InitiateList()
        {
            cmbListUrl.Items.Clear();
            foreach (string Url in DownloadListManager.Urls)
                cmbListUrl.Items.Add(Url);
            cmbListUrl.Text = Settings.Default.LastDownloadUrl;
        }

        public override void OnApplied()
        {
            Settings.Default.LastDownloadUrl = cmbListUrl.Text;
            Settings.Default.Save();

        }

        private void cmbListUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateNameDesc();
        }

        private void UpdateNameDesc()
        {
            lblListName.Text = DownloadListManager.GetListName(cmbListUrl.Text);
            lblListDescription.Text = DownloadListManager.GetListDescription(cmbListUrl.Text);
            string moreInfoUrl = DownloadListManager.GetListMoreInfoUrl(cmbListUrl.Text);
            if (lnkMoreInfo.Visible = !string.IsNullOrEmpty(moreInfoUrl))
            {
                lnkMoreInfo.Tag = moreInfoUrl;
            }

        }

        private void cmbListUrl_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(DownloadListManager.GetListName(cmbListUrl.Text)))
            {
                lblListName.Text =
                   lblListDescription.Text =
                        "برای بروزآوری نام و شرح فهرست کلیک کنید.";
            }
            else
                UpdateNameDesc();
        }

        private void RetriveNameDesc(object sender, EventArgs e)
        {
            if (cmbListUrl.SelectedIndex < 3 && cmbListUrl.Text == cmbListUrl.Items[cmbListUrl.SelectedIndex].ToString())
                return;
            string Name, Desc, MoreInfoUrl;
            if (GDBListProcessor.RetrieveProperties(cmbListUrl.Text, out Name, out Desc, out MoreInfoUrl))
            {
                DownloadListManager.Cache(cmbListUrl.Text, Name, Desc, MoreInfoUrl);
                UpdateNameDesc();
            }
        }

        private void lnkMoreInfo_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            if(lnkMoreInfo.Tag != null && lnkMoreInfo.Tag is string)
            {
                string moreInfoUrl = lnkMoreInfo.Tag as string;
                if(!string.IsNullOrEmpty(moreInfoUrl))
                    try
                    {
                        System.Diagnostics.Process.Start(moreInfoUrl);
                    }
                    catch
                    {
                        //this is normal I guess!
                    }
            }
        }



        
    }
}
