using System.Net;
using VisualPairCoding.BL;
using Newtonsoft.Json;

namespace VisualPairCoding.WinForms
{
    public partial class EnterNamesForm : Form
    {
        public EnterNamesForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var participants = new List<string>();
            AddParticipantIfAvailable(participant1Textbox.Text, participants);
            AddParticipantIfAvailable(participant2Textbox.Text, participants);
            AddParticipantIfAvailable(participant3Textbox.Text, participants);
            AddParticipantIfAvailable(participant4Textbox.Text, participants);
            AddParticipantIfAvailable(participant5Textbox.Text, participants);

            var session = new PairCodingSession(participants.ToArray(), (int)minutesPerRoundNumericUpDown.Value);
            
            var validationMessage = session.Validate();

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                MessageBox.Show(validationMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RunSessionForm form = new RunSessionForm(session);
            // Let the session window start in about the location we have right now on the screen

            Hide();
            
            try
            {
                form.Show();
                form.Top = Top;
                form.Left = Left;
                form.Hide();

                form.ShowDialog();
            }
            catch 
            {
            }

            Show();
        }

        private void AddParticipantIfAvailable(string name, List<string> participants)
        {
            if (!string.IsNullOrWhiteSpace(name))
                participants.Add(name);
        }


        private readonly string appVersion = "VisualPairCoding v1.16";
        private void EnterNamesForm_Load(object sender, EventArgs e)
        {
            var releaseURL = "https://api.github.com/repos/stho32/VisualPairCoding/releases";

            var client = new HttpClient();

            var webRequest = new HttpRequestMessage(HttpMethod.Get, releaseURL);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0");

            var response = client.Send(webRequest);

            string result = response.Content.ReadAsStringAsync().Result.Trim();
            GithubAPIResponse[] releases = JsonConvert.DeserializeObject<GithubAPIResponse[]>(result);
           
            if (releases[0].Name != appVersion)
            {
                DialogResult askFoorUserConsent =  MessageBox.Show("There is a new update, Do you want to install it now ?", "New Update", MessageBoxButtons.YesNo);

                if (askFoorUserConsent == DialogResult.OK)
                {
                    WebClient downloadClient = new WebClient();
                    downloadClient.DownloadFile(releases[0].Assets[0].Browser_download_url, releases[0].Assets[0].Name);
                    downloadClient.DownloadFileCompleted += DownloadClient_DownloadFileCompleted;
                }
                else
                {

                }
            }
        }

        private void DownloadClient_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download finished");
        }
    }
}