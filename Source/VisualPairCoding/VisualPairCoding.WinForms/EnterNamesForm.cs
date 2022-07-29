using VisualPairCoding.BL;

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
            
            Hide();
            
            try
            {
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
    }
}