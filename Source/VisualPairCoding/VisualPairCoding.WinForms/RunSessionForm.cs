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

            UpdateTurnAnimationTransparencyMenuItemText();
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

            _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            remainingTimeLabel.Text = _currentTime.ToString();
        }

        private void ChooseAnotherPairAndStartNewTurn()
        {
            timer.Stop();

            _currentTime = new TimeSpan(0, _pairCodingSession.MinutesPerTurn, 0);

            _currentParticipant += 1;
            if (_currentParticipant >= _pairCodingSession.Participants.Length)
            {
                _currentParticipant = 0;
            }

            ChooseRandomNavigatorFromListWithout(_pairCodingSession.Participants[_currentParticipant]);

            activeParticipantLabel.Text = _pairCodingSession.Participants[_currentParticipant];

            var form = new NewTurnForm(
                _pairCodingSession.Participants[_currentParticipant],
                _animationShouldBeTransparent,
                _explicitlyConfirmTurnChange
                );

            if (form.ShowDialog() == DialogResult.OK)
            {
                FlashCounter = 10;
                flashTimer.Start();
            }

            timer.Start();
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
            activeParticipantLabel.Text = _pairCodingSession.Participants[_currentParticipant];

            remainingTimeLabel.Text = _currentTime.ToString();

        }

        private void toggleWindowFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var defaultBorder = FormBorderStyle.SizableToolWindow;
            var noBorder = FormBorderStyle.None;

            FormBorderStyle = FormBorderStyle == noBorder ? defaultBorder : noBorder;
        }

        #region Enable moving by clicking into the content of the window

        private Point firstPoint; 
        private bool mouseButtonDown;
        private void RunSessionForm_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseButtonDown = true;
        }

        private void RunSessionForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseButtonDown)
            {
                // Get the difference between the two points
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                // Set the new point
                int x = Location.X - xDiff;
                int y = Location.Y - yDiff;
                Location = new Point(x, y);
            }
        }
        private void RunSessionForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseButtonDown = false;
        }

        #endregion

        #region turn animation transparency
        private bool _animationShouldBeTransparent = false;

        private void turnAnimationTransparencyMenu_Click(object sender, EventArgs e)
        {
            _animationShouldBeTransparent = !_animationShouldBeTransparent;
            UpdateTurnAnimationTransparencyMenuItemText();
        }

        private void UpdateTurnAnimationTransparencyMenuItemText()
        {
            if (_animationShouldBeTransparent)
            {
                turnAnimationTransparencyMenu.Text = "Set turn animation to not-transparent";
                return;
            }
            turnAnimationTransparencyMenu.Text = "Set turn animation to transparent";
        }

        #endregion

        private bool _explicitlyConfirmTurnChange = true;

    }
}
