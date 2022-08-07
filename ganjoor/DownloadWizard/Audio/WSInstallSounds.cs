using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ganjoor
{
    public partial class WSInstallSounds : WizardStage
    {
        public WSInstallSounds()
        {
            InitializeComponent();

            InstalledFilesCount = 0;
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
            var db = new DbBrowser();
            Application.DoEvents();
            var targetDir = DownloadableAudioListProcessor.SoundsPath;
            if (DownloadedSounds != null)
                foreach (var audioInfo in DownloadedSounds)
                {
                    grdList.Rows[grdList.Rows.Add()].Cells[0].Value = DownloadableAudioListProcessor.SuggestTitle(audioInfo);
                    var mp3FilePath = Path.Combine(targetDir, Path.GetFileName(new Uri(audioInfo["audio_mp3"]).LocalPath));
                    var poemAudio = db.AddAudio(
                        Int32.Parse(audioInfo["audio_post_ID"]),
                        mp3FilePath,
                        DownloadableAudioListProcessor.SuggestTitle(audioInfo),
                        Int32.Parse(audioInfo["audio_order"])
                        );
                    var xmlFilePath = Path.Combine(targetDir, Path.GetFileName(new Uri(audioInfo["audio_xml"]).LocalPath));
                    var lstPoemAudio = PoemAudioListProcessor.Load(xmlFilePath);
                    if (lstPoemAudio.Count == 1)
                    {
                        foreach (var xmlAudio in lstPoemAudio)
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

        public event EventHandler OnInstallStarted;
        public event EventHandler OnInstallFinished;

        public int InstalledFilesCount
        {
            get;
            private set;
        }


    }
}
