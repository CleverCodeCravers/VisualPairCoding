using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using VisualPairCoding.BL;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class AboutForm : Window
    {
        public AboutForm()
        {
            InitializeComponent();

            Url.Tapped += Url_Tapped;
            VPCVersion.Text = VersionInformation.Version;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            CloseButton.Click += CloseButton_Click;
        }

        private void Url_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            OpenUrlInBrowser("https://github.com/CleverCodeCravers/VisualPairCoding");
        }

        private void CloseButton_Click(object? sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OpenUrlInBrowser(string url)
        {
            try
            {
                // Use the default web browser to open the URL.
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(url)
                {
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur when opening the URL.
                // You can add your error handling logic here.
                Console.WriteLine($"Error opening URL: {ex.Message}");
            }
        }
    }
}
