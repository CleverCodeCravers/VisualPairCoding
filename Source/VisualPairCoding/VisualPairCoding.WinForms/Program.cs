using VisualPairCoding.BL.AutoUpdates;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.WinForms
{
    internal static class Program
    {
        static bool _autoUpdateTest = false;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var args = Environment.GetCommandLineArgs();
            _autoUpdateTest = args.Length > 1;

            // Only perform auto-updates if not in dev environment
            if (VersionInformation.Version != "$$VERSION$$" || _autoUpdateTest)
            {
                if (AutoUpdate())
                    return;
            }

            if (args.Length > 1)
            {
                var configPath = args[1];
                var form = new EnterNamesForm(true);
                form.LoadSessionIntoGUI(configPath);
                Application.Run(form);
                return;
            }

            Application.Run(new EnterNamesForm());
        }

        private static bool AutoUpdate()
        {
            var updater = new AutoUpdater(
                "VisualPairCoding",
                VersionInformation.Version,
                "https://api.github.com/repos/CleverCodeCravers/VisualPairCoding/releases");

            if (updater.IsUpdateAvailable() || _autoUpdateTest)
            {
                DialogResult userGivesConsent = MessageBox.Show("There is a new update, do you want to install it now ?", "New Update", MessageBoxButtons.YesNo);

                if (userGivesConsent == DialogResult.Yes)
                {
                    updater.Update();
                    return true;
                }
            }

            return false;
        }
    }
}