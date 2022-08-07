using System.DirectoryServices.ActiveDirectory;
using VisualPairCoding.BL;

namespace VisualPairCoding.WinForms
{
    public partial class RunSessionForm : Form
    {
        private readonly PairCodingSession _pairCodingSession;
        private TimeSpan _currentTime = TimeSpan.Zero;
        private int _currentParticipant = -1;
        private Random random = new Random();

        public RunSessionForm()
        {
            InitializeComponent();
            _pairCodingSession = new PairCodingSession(Array.Empty<string>(), 1);
        }

        public RunSessionForm(PairCodingSession pairCodingSession)
        {
            _pairCodingSession = pairCodingSession;
            InitializeComponent();
        }


        private void StopButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_currentTime <= TimeSpan.Zero)
            {
                ChooseAnotherPairAndStartNewTurn();
            }

            _currentTime = _currentTime.Subtract(new TimeSpan(0, 0, 1));
            activeParticipantLabel.Text = _pairCodingSession.Participants[_currentParticipant];

            remainingTimeLabel.Text = _currentTime.ToString();
        }

        private void ChooseAnotherPairAndStartNewTurn()
        {
            _currentParticipant += 1;
            if (_currentParticipant >= _pairCodingSession.Participants.Length)
            {
                _currentParticipant = 0;
            }

            ChooseRandomNavigatorFromListWithout(_pairCodingSession.Participants[_currentParticipant]);

            _currentTime = new TimeSpan(0, _pairCodingSession.MinutesPerTurn, 0);
            FlashCounter = 10;
            flashTimer.Start();
        }

        private void ChooseRandomNavigatorFromListWithout(string currentDriver)
        {
            var potentialNavigators = 
                _pairCodingSession.Participants.Where( x => !(x == currentDriver)).ToArray();

            var randomEntry = potentialNavigators[random.Next(0, potentialNavigators.Length)];
            recommendedNavigator.Text = randomEntry;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                PauseButton.Text = "PAUSED";
                timer.Stop();
            }
            else
            {
                PauseButton.Text = "Pause";
                timer.Start();
            }
        }

        protected int FlashCounter = 0;
        
        protected Color FlashColor1 = Color.Red;
        protected Color FlashColor2 = Color.Green;
        protected Color FlashColor3 = Color.Yellow;

        protected Color FinalColor = SystemColors.Control;

        private void flashTimer_Tick(object sender, EventArgs e)
        {
            if (FlashCounter > 0)
            {
                var selectedColor = FlashCounter % 3;
                switch (selectedColor)
                {
                    case 0:
                        BackColor = FlashColor1;
                        break;
                    case 1:
                        BackColor = FlashColor2;
                        break;
                    case 2:
                        BackColor = FlashColor3;
                        break;
                }

                FlashCounter -= 1;
                return;
            }

            flashTimer.Stop();
            BackColor = FinalColor;
        }

        private void skipCurrentDriverButton_Click(object sender, EventArgs e)
        {
            ChooseAnotherPairAndStartNewTurn();
            _currentTime = _currentTime.Subtract(new TimeSpan(0, 0, 1));
            activeParticipantLabel.Text = _pairCodingSession.Participants[_currentParticipant];

            remainingTimeLabel.Text = _currentTime.ToString();

        }

        private void toggleWindowFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var defaultBorder = FormBorderStyle.SizableToolWindow;
            var noBorder = FormBorderStyle.None;

            FormBorderStyle = FormBorderStyle == noBorder ? defaultBorder : noBorder;
        }
    }
}
