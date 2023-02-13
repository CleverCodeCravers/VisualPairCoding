using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using System;
using System.Linq;
using System.Threading.Tasks;
using VisualPairCoding.BL;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class RunSessionForm : Window
    {
        private readonly PairCodingSession _pairCodingSession;
        private TimeSpan _currentTime = TimeSpan.Zero;
        private int _currentParticipant = -1;
        private Random random = new();
        private DispatcherTimer timer;
        private bool _explicitlyConfirmTurnChange = true;

        public RunSessionForm()
        {
            InitializeComponent();
            _pairCodingSession = new PairCodingSession(Array.Empty<string>(), 1);
            this.timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            this.timer.IsEnabled = true;
            this.timer.Tick += Timer_Tick;
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
        }

        public RunSessionForm(PairCodingSession pairCodingSession)
        {
            InitializeComponent();
            this.timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            this.timer.Tick += Timer_Tick;
            this.timer.IsEnabled = true;
            _pairCodingSession = pairCodingSession;
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
        }

        private void CloseForm(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_currentTime <= TimeSpan.Zero)
            {
                ChooseAnotherPairAndStartNewTurn();
            }
            _currentTime = _currentTime.Subtract(TimeSpan.FromSeconds(1));
            remainingTimeLabel.Text = _currentTime.ToString();
        }

        private async void ChooseAnotherPairAndStartNewTurn()
        {
            timer.Stop();

            _currentTime = new TimeSpan(0, _pairCodingSession.MinutesPerTurn, 0);

            _currentParticipant += 1;
            if (_currentParticipant >= _pairCodingSession.Participants.Length)
            {
                _currentParticipant = 0;
            }

            ChooseRandomNavigatorFromListWithout(_pairCodingSession.Participants[_currentParticipant]);

            activeParticipnat.Text = _pairCodingSession.Participants[_currentParticipant];

            var form = new NewTurnForm(
                _pairCodingSession.Participants[_currentParticipant],
                _explicitlyConfirmTurnChange
                );

            var tcs = new TaskCompletionSource<object>();
            form.Closed += (s, e) => tcs.SetResult(null);

            form.Show();
            await tcs.Task;
            timer.Start();
        }

        private void ChooseRandomNavigatorFromListWithout(string currentDriver)
        {
            var potentialNavigators =
                _pairCodingSession.Participants.Where(x => !(x == currentDriver)).ToArray();

            var randomEntry = potentialNavigators[random.Next(0, potentialNavigators.Length)];
            recommendedNavigator.Text = randomEntry;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs args)
        {
            if (timer.IsEnabled)
            {
                PauseButton.Content = "PAUSED";
                timer.Stop();
            }
            else
            {
                PauseButton.Content = "Pause";
                timer.Start();
            }
        }

        private void skipCurrentDriverButton_Click(object sender, RoutedEventArgs args)
        {
            ChooseAnotherPairAndStartNewTurn();
            activeParticipnat.Text = _pairCodingSession.Participants[_currentParticipant];

            remainingTimeLabel.Text = _currentTime.ToString();

        }


    }
}
