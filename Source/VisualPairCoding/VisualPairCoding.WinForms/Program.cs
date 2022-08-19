using VisualPairCoding.BL.AutoUpdates;
using VisualPairCoding.Infrastructure;

namespace VisualPairCoding.WinForms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Only perform auto-updates if not in dev environment
            if (VersionInformation.Version != "$$VERSION$$")
                AutoUpdate();

            Application.Run(new EnterNamesForm());
        }

        private static void AutoUpdate()
        {
            var updater = new AutoUpdater(
                "VisualPairCoding",
                VersionInformation.Version,
                "https://api.github.com/repos/stho32/VisualPairCoding/releases");

            if (updater.IsUpdateAvailable())
            {
                DialogResult userGivesConsent = MessageBox.Show("There is a new update, do you want to install it now ?", "New Update", MessageBoxButtons.YesNo);

                if (userGivesConsent == DialogResult.Yes)
                {
                    updater.Update();
                    Application.Exit();
                }
            }
        }
    }
}