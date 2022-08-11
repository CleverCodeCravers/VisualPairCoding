using System.Diagnostics;
using System.Net;
using VisualPairCoding.BL;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.WinForms
{
    public partial class EnterNamesForm : Form
    {
        private readonly string appVersion = "VisualPairCoding v1.19";

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

        private void EnterNamesForm_Load(object sender, EventArgs e)
        {
            AutoUpdater updater = new AutoUpdater(appVersion);
            updater.RegisterVersionInRegistery();
            bool NewUpdate = updater.IsUpdateAvailable();

            if (NewUpdate)
            {
                DialogResult askFoorUserConsent = MessageBox.Show("There is a new update, Do you want to install it now ?", "New Update", MessageBoxButtons.YesNo);

                if (askFoorUserConsent == DialogResult.Yes)
                {
                    updater.Update();
                    var cwd = Directory.GetCurrentDirectory();
                    string path = cwd + "\\" + "updater.ps1";

                    var script =
                    "Set-Location $PSScriptRoot" + Environment.NewLine +
                    "Expand-Archive -Path \"$pwd\\VisualPairCoding-win-x64.zip\" -DestinationPath $pwd -Force" + Environment.NewLine +
                    "Invoke-Expression -Command \"$pwd\\VisualPairCoding.WinForms.exe\"" + Environment.NewLine +
                    "Remove-Item -Path \"$pwd\\VisualPairCoding-win-x64.zip\" -Force" + Environment.NewLine +
                    "Remove-Item -Path \"$pwd\\updater.ps1\" -Force";


                     File.WriteAllText("updater.ps1", script);
                    try
                    {
                        var startInfo = new ProcessStartInfo()
                        {
                            FileName = "powershell.exe",
                            Arguments = $"-NoProfile -ExecutionPolicy ByPass -File \"{path}\"",
                            UseShellExecute = false
                        };
                        Application.Exit();
                        Process.Start(startInfo);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }

            }
        }
    }
}