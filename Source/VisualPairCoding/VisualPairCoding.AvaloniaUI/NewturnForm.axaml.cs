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

        public NewTurnForm(string message, bool explicitChange)
        {
            Topmost = true;
            _explicitChange = explicitChange;
            InitializeComponent();

            messageLabel.Text = message;
            WindowState = WindowState.Maximized;
            OkButton.IsVisible = explicitChange;
            ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
            ExtendClientAreaTitleBarHeightHint = -1;
            ExtendClientAreaToDecorationsHint = true;
            this.PointerPressed += onPointerPressed;
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
