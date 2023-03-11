using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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
        private readonly Random random = new();
        private readonly DispatcherTimer timer;
        private readonly bool _explicitlyConfirmTurnChange = true;

        public RunSessionForm()
        {
            InitializeComponent();
        }

        public RunSessionForm(PairCodingSession pairCodingSession)
        {
            InitializeComponent();
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.IsEnabled = true;
            _pairCodingSession = pairCodingSession;
            ExtendClientAreaToDecorationsHint = true;
            ExtendClientAreaChromeHints = Avalonia.Platform.ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            PointerPressed += OnPointerPressed;
            PointerMoved += OnPointerMoved;
            PointerReleased += OnPointerReleased;
            PointerLeave += OnPointerLeave;
        }

        private Point _firstPoint;
        private bool _mouseButtonDown;

        private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
            {
                _firstPoint = e.GetPosition(this);
                _mouseButtonDown = true;
            }
        }

        private void OnPointerMoved(object? sender, PointerEventArgs e)
        {
            if (_mouseButtonDown && e.Pointer.Type == PointerType.Mouse)
            {
                var currentPoint = e.GetPosition(this);
                var xDiff = _firstPoint.X - currentPoint.X;
                var yDiff = _firstPoint.Y - currentPoint.Y;

                var x = (int)(Position.X - xDiff);
                var y = (int)(Position.Y - yDiff);
                Position = new PixelPoint(x, y);
            }
        }

        private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
        {
            _mouseButtonDown = false;
        }

        private void OnPointerLeave(object? sender, PointerEventArgs e)
        {
            if (_mouseButtonDown)
            {
                _mouseButtonDown = false;
            }
        }

        private void CloseForm(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void Timer_Tick(object? sender, EventArgs e)
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
            form.Closed += (s, e) => tcs.SetResult(null!);

            form.Show();
            form.Topmost = true;
            await tcs.Task;
            timer.Start();
            Topmost = true;
        }

        private void ChooseRandomNavigatorFromListWithout(string currentDriver)
        {
            var potentialNavigators =
                _pairCodingSession.Participants.Where(x => !(x == currentDriver)).ToArray();

            var randomEntry = potentialNavigators[random.Next(0, potentialNavigators.Length)];
            recommendedNavigator.Text = randomEntry;
        }

        private void PauseButton_Click(object? sender, RoutedEventArgs args)
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

        private void SkipCurrentDriverButton_Click(object? sender, RoutedEventArgs args)
        {
            Topmost = false;
            ChooseAnotherPairAndStartNewTurn();
            activeParticipnat.Text = _pairCodingSession.Participants[_currentParticipant];

            remainingTimeLabel.Text = _currentTime.ToString();

        }


    }
}
