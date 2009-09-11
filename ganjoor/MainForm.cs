using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Net;
using System.Xml;
using System.IO;
using System.Reflection;
using ganjoor.Properties;

/*
 * Version Pre 1.0 -> 1388/04/29
 * Hamid Reza Mohammadi http://ganjoor.net
 *
 * Version 1.0:
 *   Changelog (after a pre 1.0 release):   
 *      MainForm code moved to GanjoorView user control
 *      Added main toolbar, statusbar, about box
 *      Project code moved to sf.net : http://ganjoor.sourceforge.net
 */

namespace ganjoor
{
    public partial class MainForm : Form
    {
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

        private void ApplyUserSettings()
        {
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
            processTextChanged = false;
            mnuShowBeytNums.Checked = btnShowBeytNums.Checked = Settings.Default.ShowBeytNums;
            processTextChanged = true;
            ganjoorView.ApplyUISettings();
            ganjoorView.Invalidate();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowHome(true);
        }

        private void ganjoorView_OnPageChanged(string PageString, bool HasComments, bool CanBrowse, bool IsFaved, bool FavsPage, string HighlightedText, object preItem, object nextItem)
        {            
            lblCurrentPage.Text = PageString;
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
                btnNextPoem.Text = "صفحۀ بعد";
                btnNextPoem.Enabled = nextItem != null;
                btnNextPoem.Tag = nextItem;
                btnPreviousPoem.Text = "صفحۀ قبل";
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
            btnHighlight.Enabled = HasComments;

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
                    processTextChanged = true;
                    lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", ganjoorView.HighlightText(HighlightedText));
                }
                else
                {
                    processTextChanged = false;
                    txtHighlight.Text = "";
                    processTextChanged = true;
                    ganjoorView.HighlightText(HighlightedText);
                    highlight = false;
                }
            }
            btnHighlight.Checked = highlight;
            lblFoundItemCount.Visible = highlight;


            ganjoorView.Focus();

            
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
            System.Diagnostics.Process.Start(ganjoorView.CurrentPageGanjoorUrl);
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(ganjoorView.CurrentPoemCommentsUrl);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            using (PrintDialog dlg = new PrintDialog())
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {                    
                    using (dlg.Document = new System.Drawing.Printing.PrintDocument())
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
                    Properties.Settings.Default.SearchPageItems = dlg.ItemsInPage;
                    Properties.Settings.Default.Save();
                    ganjoorView.ShowSearchResults(dlg.Phrase, 0, dlg.ItemsInPage, ganjoorView.GetPoetID(dlg.PoetOrder));
                }
            }
        }

        private void btnCopyText_Click(object sender, EventArgs e)
        {
            ganjoorView.CopyText();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ganjoorView.StoreSettings();
            Properties.Settings.Default.WindowMaximized = (this.WindowState == FormWindowState.Maximized);
            Properties.Settings.Default.WindowLocation = this.Location;
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();
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
                lblFoundItemCount.Visible = false;
                processTextChanged = true;
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

        private bool processTextChanged = true;
        private void txtHighlight_TextChanged(object sender, EventArgs e)
        {
            if (processTextChanged)
            {
                int count = ganjoorView.HighlightText(txtHighlight.Text);
                if (lblFoundItemCount.Visible = !string.IsNullOrEmpty(txtHighlight.Text))
                    lblFoundItemCount.Text = String.Format("{0} مورد یافت شد.", count);
            }
        }

        private void btnFavUnFav_Click(object sender, EventArgs e)
        {
            bool result = ganjoorView.ToggleFav();
            mnuFavUnFav.Image = btnFavUnFav.Image = result ? Properties.Resources.favorite_remove : Properties.Resources.favorite_add;
            mnuFavUnFav.Text = btnFavUnFav.Text = result ? "حذف نشانه" : "نشانه‌گذاری";
            mnuFavUnFav.Checked = btnFavUnFav.Checked = result;
        }

        private void btnFavs_Click(object sender, EventArgs e)
        {
            ganjoorView.ShowFavs(0, 10);
        }

        private void btnShowBeytNums_CheckedChanged(object sender, EventArgs e)
        {
            if (processTextChanged)                
                ganjoorView.ToggleBeytNums();
        }

        private void mnuShowBeytNums_Click(object sender, EventArgs e)
        {
            mnuShowBeytNums.Checked = btnShowBeytNums.Checked = !btnShowBeytNums.Checked;            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
            if (Settings.Default.CheckForUpdate)
                CheckForUpdate(false);
        }

        private void btnCheckForUpdate_Click(object sender, EventArgs e)
        {
            CheckForUpdate(true);
        }

        private static void CheckForUpdate(bool Prompt)
        {
            try
            {
                WebRequest req = WebRequest.Create("http://ganjoor.sourceforge.net/version.xml");
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
                                if (Node.Name == "Major")
                                    VersionMajor = Convert.ToInt32(Node.InnerText);
                                else
                                    if (Node.Name == "Minor")
                                        VersionMinor = Convert.ToInt32(Node.InnerText);
                                    else
                                        if (Node.Name == "UpdateUrl")
                                            updateUrl = Node.InnerText;
                            if (VersionMajor == MyVersionMajor && VersionMinor == MyVersionMinor)
                            {
                                if (Prompt)
                                {
                                    MessageBox.Show("شما آخرین ویرایش گنجور رومیزی را در اختیار دارید.", "تبریک", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign);
                                }
                            }
                            else
                                if (
                                MessageBox.Show("ویرایش جدیدتر "+VersionMajor.ToString()+"."+VersionMinor.ToString()+" از نرم‌افزار ارائه شده است. صفحۀ دریافت باز شود؟ ", "ویرایش جدید", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign)
                                   ==
                                   DialogResult.Yes
                                    )
                                {
                                    System.Diagnostics.Process.Start(updateUrl);
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
    }
}
