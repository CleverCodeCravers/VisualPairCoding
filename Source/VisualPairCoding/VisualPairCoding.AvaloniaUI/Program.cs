using Avalonia;
using Avalonia.Controls;
using System;

namespace VisualPairCoding.AvaloniaUI
{
    internal class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            BuildAvaloniaApp().Start(AppMain, args);
        }

        static void AppMain(Application app, string[] args)
        {
            app.Initialize();

            bool autostart = false;
            string configPath = string.Empty;
            
            if (IsStartupWithSessionFile(args))
            {
                autostart = true;
                configPath = args[0];
            }

            var enterNamesForm = new EnterNamesForm(autostart, configPath);
            app.Run(enterNamesForm);
        }

        private static bool IsStartupWithSessionFile(string[] args)
        {
            return (args.Length >= 1);
        }


        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }

    }
}
