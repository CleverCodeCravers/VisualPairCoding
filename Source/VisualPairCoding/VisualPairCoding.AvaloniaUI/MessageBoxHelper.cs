using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Layout;
using System.Threading.Tasks;

namespace VisualPairCoding.AvaloniaUI
{
    public static class MessageBoxHelper
    {
        private static async Task<bool> ShowDialog(Window owner, string title, string message, bool isError)
        {
            var dialog = new Window
            {
                Title = title,
                Width = 420,
                Height = 220,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                CanResize = false,
                ShowInTaskbar = false,
                SystemDecorations = SystemDecorations.BorderOnly,
                ExtendClientAreaToDecorationsHint = true,
                ExtendClientAreaTitleBarHeightHint = -1,
                Background = new SolidColorBrush(Color.FromRgb(245, 245, 245))
            };

            // Main container with border
            var border = new Border
            {
                BorderBrush = new SolidColorBrush(isError ? Color.FromRgb(220, 53, 69) : Color.FromRgb(40, 167, 69)),
                BorderThickness = new Avalonia.Thickness(0, 3, 0, 0),
                Background = Brushes.White,
                CornerRadius = new Avalonia.CornerRadius(0)
            };

            var mainPanel = new DockPanel
            {
                LastChildFill = true
            };

            // Title bar
            var titlePanel = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(240, 240, 240)),
                Height = 40,
                [DockPanel.DockProperty] = Dock.Top
            };

            var titleText = new TextBlock
            {
                Text = title,
                FontSize = 14,
                FontWeight = FontWeight.SemiBold,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Avalonia.Thickness(15, 0, 0, 0)
            };

            titlePanel.Child = titleText;
            mainPanel.Children.Add(titlePanel);

            // Content area
            var contentPanel = new Grid
            {
                Margin = new Avalonia.Thickness(25, 20, 25, 20),
                RowDefinitions = new RowDefinitions("*, Auto")
            };

            // Icon and message panel
            var messagePanel = new DockPanel
            {
                [Grid.RowProperty] = 0,
                Margin = new Avalonia.Thickness(0, 0, 0, 20)
            };

            // Icon
            var iconBorder = new Border
            {
                Width = 48,
                Height = 48,
                CornerRadius = new Avalonia.CornerRadius(24),
                Background = new SolidColorBrush(isError ? Color.FromRgb(255, 235, 238) : Color.FromRgb(232, 246, 234)),
                [DockPanel.DockProperty] = Dock.Left,
                Margin = new Avalonia.Thickness(0, 0, 15, 0)
            };

            var iconText = new TextBlock
            {
                Text = isError ? "!" : "✓",
                FontSize = 24,
                FontWeight = FontWeight.Bold,
                Foreground = new SolidColorBrush(isError ? Color.FromRgb(220, 53, 69) : Color.FromRgb(40, 167, 69)),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            iconBorder.Child = iconText;
            messagePanel.Children.Add(iconBorder);

            // Message text
            var messageText = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 13,
                VerticalAlignment = VerticalAlignment.Center,
                LineHeight = 20
            };

            messagePanel.Children.Add(messageText);
            contentPanel.Children.Add(messagePanel);

            // Button panel
            var buttonPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                [Grid.RowProperty] = 1
            };

            var button = new Button
            {
                Content = "OK",
                Width = 90,
                Height = 32,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                Background = new SolidColorBrush(isError ? Color.FromRgb(220, 53, 69) : Color.FromRgb(40, 167, 69)),
                Foreground = Brushes.White,
                FontWeight = FontWeight.Medium,
                CornerRadius = new Avalonia.CornerRadius(4)
            };

            button.Classes.Add("primary");
            button.Click += (s, e) => dialog.Close();

            buttonPanel.Children.Add(button);
            contentPanel.Children.Add(buttonPanel);

            mainPanel.Children.Add(contentPanel);
            border.Child = mainPanel;
            dialog.Content = border;

            await dialog.ShowDialog(owner);
            return true;
        }

        public static async Task<bool> ShowError(Window owner, string title, string message)
        {
            return await ShowDialog(owner, title, message, true);
        }

        public static async Task<bool> ShowInfo(Window owner, string title, string message)
        {
            return await ShowDialog(owner, title, message, false);
        }
    }
}