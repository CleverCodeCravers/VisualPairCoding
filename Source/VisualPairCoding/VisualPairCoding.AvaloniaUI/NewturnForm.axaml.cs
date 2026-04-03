using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class NewTurnForm : Window
    {
        private readonly bool _explicitChange;

        public NewTurnForm()
        {
            InitializeComponent();
        }

        public NewTurnForm(string message, bool explicitChange, PixelRect? targetScreenBounds = null)
        {
            Topmost = true;
            _explicitChange = explicitChange;
            InitializeComponent();

            messageLabel.Text = message;
            OkButton.IsVisible = explicitChange;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            ExtendClientAreaToDecorationsHint = true;
            this.PointerPressed += onPointerPressed;

            if (targetScreenBounds.HasValue)
            {
                PositionOnScreen(targetScreenBounds.Value);
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void PositionOnScreen(PixelRect bounds)
        {
            WindowStartupLocation = WindowStartupLocation.Manual;
            Position = new PixelPoint(bounds.X, bounds.Y);
            Width = bounds.Width;
            Height = bounds.Height;
            WindowState = WindowState.Normal;
        }

        private void onPointerPressed(object? sender, PointerEventArgs e)
        {
            e.Handled = true;
        }

        private void CloseForm(object sender, RoutedEventArgs args)
        {
            Close();
        }
    }
}
