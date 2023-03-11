using System.Text.Json;

namespace VisualPairCoding.Infrastructure
{
    public static class SessionConfigurationFileHandler
    {
        public static string GetFilenameProposal(string[] participants)
        {
            string configName = string.Join("_", participants);
            return configName + ".vpcsession";
        }
        
        public static SessionConfiguration Load(string filePath)
        {
            var configFile = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<SessionConfiguration>(configFile)!;
        }

        public static void Save(string filePath, SessionConfiguration serviceConfiguration)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(serviceConfiguration, options);
            File.WriteAllText(filePath, json);
        }
    }
}