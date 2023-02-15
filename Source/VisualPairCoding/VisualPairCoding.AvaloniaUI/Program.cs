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

            // Only perform auto-updates if not in dev environment
            //if (VersionInformation.Version != "$$VERSION$$")
            //{
            //    AutoUpdate();
            //}

            if (args.Length >= 1)
            {
                var configPath = args[0];
                var form = new EnterNamesForm(true);
                form.LoadSessionIntoGui(configPath);
                app.Run(form);
                return;
            }

            app.Run(new EnterNamesForm());
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace();
        }


        private static bool AutoUpdate()
        {
            var updater = new AutoUpdater(
                "VisualPairCoding",
                VersionInformation.Version,
                "https://api.github.com/repos/CleverCodeCravers/VisualPairCoding/releases");

            if (updater.IsUpdateAvailable())
            {
                var messageBox = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow("Info", "There is a new update, do you want to install it now ?", MessageBox.Avalonia.Enums.ButtonEnum.YesNo, MessageBox.Avalonia.Enums.Icon.Info, WindowStartupLocation.CenterScreen);
                var result = messageBox.Show();

                if (result.ToString() == "Yes")
                {
                    updater.Update();
                    return true;
                }

            }

            return false;
        }
    }
}
