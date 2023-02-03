using VisualPairCoding.BL;
using VisualPairCoding.BL.AutoUpdates;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.WinForms
{
    public partial class EnterNamesForm : Form
    {
        private bool _autostart;
        public EnterNamesForm()
        {
            InitializeComponent();

        }

        public EnterNamesForm(bool autostart)
        {
            InitializeComponent();
            _autostart = autostart;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            var participants =  GetParticipants();

            var session = new PairCodingSession(participants.ToArray(), (int)minutesPerRoundNumericUpDown.Value);
            
            var validationMessage = session.Validate();

            if (!string.IsNullOrWhiteSpace(validationMessage))
            {
                MessageBox.Show(validationMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Hide();
            
            try
            {
                using (RunSessionForm form = new RunSessionForm(session))
                {
                    form.Show();
                    // Let the session window start in about the location we have right now on the screen
                    form.Top = Top;
                    form.Left = Left;
                    form.Hide();

                    form.ShowDialog();
                }
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

        private List<string> GetParticipants()
        {
            var participants = new List<string>();
            AddParticipantIfAvailable(participant1Textbox.Text, participants);
            AddParticipantIfAvailable(participant2Textbox.Text, participants);
            AddParticipantIfAvailable(participant3Textbox.Text, participants);
            AddParticipantIfAvailable(participant4Textbox.Text, participants);
            AddParticipantIfAvailable(participant5Textbox.Text, participants);
            AddParticipantIfAvailable(participant6Textbox.Text, participants);
            AddParticipantIfAvailable(participant7Textbox.Text, participants);
            AddParticipantIfAvailable(participant8Textbox.Text, participants);
            AddParticipantIfAvailable(participant9Textbox.Text, participants);
            AddParticipantIfAvailable(participant10Textbox.Text, participants);

            return participants;
        }

        private void randomizeParticipantsButton_Click(object sender, EventArgs e)
        {
            var participants = GetParticipants();


            participant1Textbox.Text = "";
            participant2Textbox.Text = "";
            participant3Textbox.Text = "";
            participant4Textbox.Text = "";
            participant5Textbox.Text = "";
            participant6Textbox.Text = "";
            participant7Textbox.Text = "";
            participant8Textbox.Text = "";
            participant9Textbox.Text = "";
            participant10Textbox.Text = "";

            Shuffle(participants);

            if (participants.Count > 0) participant1Textbox.Text = participants[0];
            if (participants.Count > 1) participant2Textbox.Text = participants[1];
            if (participants.Count > 2) participant3Textbox.Text = participants[2];
            if (participants.Count > 3) participant4Textbox.Text = participants[3];
            if (participants.Count > 4) participant5Textbox.Text = participants[4];
            if (participants.Count > 5) participant6Textbox.Text = participants[5];
            if (participants.Count > 6) participant7Textbox.Text = participants[6];
            if (participants.Count > 7) participant8Textbox.Text = participants[7];
            if (participants.Count > 8) participant9Textbox.Text = participants[8];
            if (participants.Count > 9) participant10Textbox.Text = participants[9];
        }

        private static Random random = new Random();

        public void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void infoMenuItem_Click(object sender, EventArgs e)
        {
            var aboutForn = new AboutForm(VersionInformation.Version, "https://github.com/stho32/VisualPairCoding", "VisualPairCoding");
            aboutForn.ShowDialog();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var participants = GetParticipants();


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.DefaultExt = ".json";
            saveFileDialog.FileName = "config.json";
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;

                try
                {
                    SessionConfigurationFileHandler.Save(fileName, new SessionConfiguration(participants, (int)minutesPerRoundNumericUpDown.Value));
                    MessageBox.Show("Session Configuration saved successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving configuration File: " + ex.Message);
                }
            }

        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.DefaultExt = ".json";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = openFileDialog.FileName;

                LoadSessionIntoGUI(fileName);
            }
        }

        public void LoadSessionIntoGUI(string fileName)
        {
            try
            {
                var session = SessionConfigurationFileHandler.Load(fileName);

                for (var i = 1; i < session.Participants.Count; i++)
                {
                    if (this.Controls.Find("participant" + i + "Textbox", true).FirstOrDefault() is TextBox participantTextbox)
                    {
                        participantTextbox.Text = session.Participants[i].Trim();
                    }

                }

                minutesPerRoundNumericUpDown.Value = session.SessionLength;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Loading configuration File: " + ex.Message);
            }
        }

        private void EnterNamesForm_Load(object sender, EventArgs e)
        {
            if (_autostart)
            {
                this.startButton_Click(sender, e);
            }
        }
    }
}