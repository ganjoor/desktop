namespace ganjoor.Audio_Support.TimingHelper
{
    public partial class THFirstVerse : WizardStage
    {
        public THFirstVerse(int nPoemId = 0)
        {
            Font = Properties.Settings.Default.ViewFont;

            InitializeComponent();

            DbBrowser dbBrowser = new DbBrowser();

            var verses = dbBrowser.GetVerses(nPoemId);

            if(verses.Count > 0)
            {
                lblVerse.Text = verses[0]._Text;
            }

            dbBrowser.CloseDb();

        }
    }
}
