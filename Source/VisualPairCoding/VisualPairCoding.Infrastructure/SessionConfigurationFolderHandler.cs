using VisualPairCoding.Infrastructure;

public static class SessionConfigurationFolderHandler
{
    public static string GetEnvironnementAppDataFolder()
    {
        string appDataFolderPath;

        if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
        {
            appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
        else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
        {
            appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }
        else
        {
            appDataFolderPath = "";
        }

        return appDataFolderPath!;
    }

    public static bool CheckIfConfigurationFolderExistsUnderAppDataFolder()
    {
        var folderPath = GetSessionFolderPath();

        if (!Directory.Exists(folderPath))
        {
            return false;
        }

        return true;
    }

    public static void CreateConfigurationFolderUnderAppDataFolder()
    {
        var folderPath = GetSessionFolderPath();
        Directory.CreateDirectory(folderPath);

        return;
    }

    public static string GetSessionFolderPath()
    {
        var appdataDir = GetEnvironnementAppDataFolder();
        var folderPath = Path.Combine(appdataDir, "VisualPairCoding");

        return folderPath;
    }

    public static List<string> GetConfigurationFiles()
    {
        var configDir = GetSessionFolderPath();
        List<string> configs = new();

        string[] fileNames = Directory.GetFiles(configDir);

        foreach (string fileName in fileNames)
        {
            configs.Add(Path.GetFileName(fileName.Replace(".vpcsession", "")));
        }

        return configs;
    }
}