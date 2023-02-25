using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Threading;
using System.Threading.Tasks;
using VisualPairCoding.BL.AutoUpdates;
using VisualPairCoding.Infrastructure;

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

            var enterNamesForm = new EnterNamesForm();

            if (IsAutoUpdateShouldBeExecutedExplicitlySet(args))
            {
                AutoUpdateDetector.setUpdateIsAvailable();
            }
            
            if (IsStartupWithSessionFile(args))
            {
                var configPath = args[0];

                enterNamesForm = new EnterNamesForm(true);
                enterNamesForm.LoadSessionIntoGui(configPath);
            }

            app.Run(enterNamesForm);
        }

        private static bool IsStartupWithSessionFile(string[] args)
        {
            return (args.Length >= 1 && args[0] != "AutoUpdate");
        }

        private static bool IsAutoUpdateShouldBeExecutedExplicitlySet(string[] args)
        {
            return (args.Length >= 1 && args[0] == "AutoUpdate");
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }

    }
}
