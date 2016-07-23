using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ganjoor
{
    public partial class WSInstallSounds : WizardStage
    {
        public WSInstallSounds()
        {
            InitializeComponent();

            this.InstalledFilesCount = 0;
        }

        public override bool PreviousStageButton
        {
            get
            {
                return false;
            }
        }

        public List<Dictionary<string, string>> DownloadedSounds
        {
            set;
            get;
        }

        public override void OnActivated()
        {
            if (OnInstallStarted != null)
                OnInstallStarted(this, new EventArgs());
            DbBrowser db = new DbBrowser();
            Application.DoEvents();
            string targetDir = DownloadableAudioListProcessor.SoundsPath;
            if (DownloadedSounds != null)
                foreach (Dictionary<string, string> audioInfo in DownloadedSounds)
                {
                    grdList.Rows[grdList.Rows.Add()].Cells[0].Value = DownloadableAudioListProcessor.SuggestTitle(audioInfo);
                    string mp3FilePath = Path.Combine(targetDir, Path.GetFileName(new Uri(audioInfo["audio_mp3"]).LocalPath));
                    PoemAudio poemAudio =  db.AddAudio(
                        Int32.Parse(audioInfo["audio_post_ID"]), 
                        mp3FilePath, 
                        DownloadableAudioListProcessor.SuggestTitle(audioInfo),
                        Int32.Parse(audioInfo["audio_order"])
                        );
                    string xmlFilePath = Path.Combine(targetDir, Path.GetFileName(new Uri(audioInfo["audio_xml"]).LocalPath));
                    List<PoemAudio> lstPoemAudio = PoemAudioListProcessor.Load(xmlFilePath);
                    if (lstPoemAudio.Count == 1)
                    {
                        foreach (PoemAudio xmlAudio in lstPoemAudio)
                        {
                            if (xmlAudio.PoemId == poemAudio.PoemId)
                            {                                
                                poemAudio.SyncArray = xmlAudio.SyncArray;                                
                                db.SavePoemSync(poemAudio, poemAudio.SyncArray, false);
                                poemAudio.SyncGuid = Guid.Parse(audioInfo["audio_guid"]);
                                db.WritePoemAudioGuid(poemAudio);
                                db.DeleteAudioWithSync(xmlAudio.PoemId, xmlAudio.SyncGuid, poemAudio.Id);
                                File.Delete(xmlFilePath); //not needed any more
                                grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "نصب شد.";
                                InstalledFilesCount++;
                            }
                        }
                    }
                    else
                    {
                        grdList.Rows[grdList.RowCount - 1].Cells[1].Value = "خطا: فایل xml محتوی اطلاعات همگامسازی یک سطر نیست.";
                    }
                    Application.DoEvents();
                }
            db.CloseDb();
            if (OnInstallFinished != null)
                OnInstallFinished(this, new EventArgs());
        }

        public event EventHandler OnInstallStarted = null;
        public event EventHandler OnInstallFinished = null;

        public int InstalledFilesCount
        {
            get;
            private set;
        }


    }
}
