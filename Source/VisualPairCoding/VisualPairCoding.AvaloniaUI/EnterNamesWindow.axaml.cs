using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace VisualPairCoding.AvaloniaUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        
        public void ClickDenHandler(object? sender, RoutedEventArgs args)
        {
            Console.WriteLine("Hello");
        }
    }
}
