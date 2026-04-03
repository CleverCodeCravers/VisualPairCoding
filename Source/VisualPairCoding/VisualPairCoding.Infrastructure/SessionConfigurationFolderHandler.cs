using System.Runtime.InteropServices;

namespace VisualPairCoding.Infrastructure;

public class SessionConfigurationFolderHandler
{
    private readonly string _sessionFolderPath;
    private readonly SessionConfigurationFileHandler _fileHandler;

    public SessionConfigurationFolderHandler()
        : this(GetDefaultSessionFolderPath(), new SessionConfigurationFileHandler())
    {
    }

    public SessionConfigurationFolderHandler(string sessionFolderPath, SessionConfigurationFileHandler fileHandler)
    {
        _sessionFolderPath = sessionFolderPath;
        _fileHandler = fileHandler;
    }

    private static string GetDefaultSessionFolderPath()
    {
        var appdataDir = GetEnvironmentAppDataFolder();
        if (string.IsNullOrEmpty(appdataDir))
            return string.Empty;

        return Path.Combine(appdataDir, "VisualPairCoding");
    }

    private static string GetEnvironmentAppDataFolder()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        return string.Empty;
    }

    public string[] GetRecentSessionNames()
    {
        if (string.IsNullOrEmpty(_sessionFolderPath))
            return Array.Empty<string>();

        if (!Path.Exists(_sessionFolderPath))
            return Array.Empty<string>();

        List<string> configs = new();

        string[] fileNames = Directory.GetFiles(_sessionFolderPath);

        foreach (string fileName in fileNames)
        {
            configs.Add(Path.GetFileName(fileName.Replace(".vpcsession", "")));
        }

        return configs.ToArray();
    }

    public void SaveAsRecentSession(SessionConfiguration sessionConfiguration)
    {
        if (string.IsNullOrEmpty(_sessionFolderPath))
            return;

        if (!Directory.Exists(_sessionFolderPath))
        {
            Directory.CreateDirectory(_sessionFolderPath);
        }

        var fileNameProposal = _fileHandler.GetFilenameProposal(sessionConfiguration.Participants);
        var filePath = Path.Combine(_sessionFolderPath, fileNameProposal);
        _fileHandler.Save(filePath, sessionConfiguration);
    }

    public SessionConfiguration LoadRecentSession(string recentSessionName)
    {
        var configPath = Path.Combine(_sessionFolderPath, recentSessionName + ".vpcsession");
        return _fileHandler.Load(configPath);
    }
}
