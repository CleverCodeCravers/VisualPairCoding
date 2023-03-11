using System.Runtime.InteropServices;

namespace VisualPairCoding.Infrastructure;

public static class SessionConfigurationFolderHandler
{
    private static string GetEnvironmentAppDataFolder()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return string.Empty;
    }

    private static bool CheckIfConfigurationFolderExistsUnderAppDataFolder()
    {
        var folderPath = GetSessionFolderPath();

        if (!Directory.Exists(folderPath))
        {
            return false;
        }

        return true;
    }

    private static void CreateConfigurationFolderUnderAppDataFolder()
    {
        var folderPath = GetSessionFolderPath();
        Directory.CreateDirectory(folderPath);

        return;
    }

    private static string GetSessionFolderPath()
    {
        var appdataDir = GetEnvironmentAppDataFolder();
        if (string.IsNullOrEmpty(appdataDir))
            return string.Empty;
        
        var folderPath = Path.Combine(appdataDir, "VisualPairCoding");

        return folderPath;
    }

    public static string[] GetRecentSessionNames()
    {
        var configDir = GetSessionFolderPath();
        if (string.IsNullOrEmpty(configDir))
            return Array.Empty<string>();
        
        if (!Path.Exists(configDir))
            return Array.Empty<string>();
        
        List<string> configs = new();

        string[] fileNames = Directory.GetFiles(configDir);

        foreach (string fileName in fileNames)
        {
            configs.Add(Path.GetFileName(fileName.Replace(".vpcsession", "")));
        }

        return configs.ToArray();
    }

    public static void SaveAsRecentSession(SessionConfiguration sessionConfiguration)
    {
        var appDataPath = GetSessionFolderPath();
        
        if (!CheckIfConfigurationFolderExistsUnderAppDataFolder())
        {
            CreateConfigurationFolderUnderAppDataFolder();
        }
        
        if (!string.IsNullOrEmpty(appDataPath))
        {
            var fileNameProposal =
                SessionConfigurationFileHandler.GetFilenameProposal(sessionConfiguration.Participants);
            var filePath = Path.Combine(appDataPath, fileNameProposal);
            SessionConfigurationFileHandler.Save(filePath, sessionConfiguration);
        }
    }

    public static SessionConfiguration LoadRecentSession(string recentSessionName)
    {
        var configPath = Path.Combine(GetSessionFolderPath(), recentSessionName + ".vpcsession");
        var sessionConfiguration = SessionConfigurationFileHandler.Load(configPath);
        return sessionConfiguration;
    }
}