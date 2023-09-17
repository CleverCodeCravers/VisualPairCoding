using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class AboutForm : Window
    {
        public AboutForm()
        {
            InitializeComponent();

            NaseifsUrl.Tapped += NaseifsUrl_Tapped;
        }

        private void NaseifsUrl_Tapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            OpenUrlInBrowser("https://github.com/naseif");
        }

        private void Naseif_OnClick(object sender, RoutedEventArgs e)
        {
            // Open Naseif's GitHub page
        }

        private void Stho32_OnClick(object sender, RoutedEventArgs e)
        {
            // Open stho32's GitHub page
            OpenUrlInBrowser("https://github.com/stho32");
        }

        private void GitHubRepo_OnClick(object sender, RoutedEventArgs e)
        {
            // Open your GitHub repository page
            OpenUrlInBrowser("https://github.com/yourrepository");
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
