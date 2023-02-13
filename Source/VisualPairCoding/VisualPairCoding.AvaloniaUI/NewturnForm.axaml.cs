using Avalonia.Controls;
using Avalonia.Interactivity;

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
            _explicitChange = explicitChange;
            InitializeComponent();

            messageLabel.Text = message;
            this.WindowState = WindowState.Maximized;
            OkButton.IsVisible = explicitChange;
        }

        private void CloseForm(object sender, RoutedEventArgs args)
        {
            Close();
        }


    }
}
