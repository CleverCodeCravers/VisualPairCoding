using System.Text.Json;

namespace VisualPairCoding.Infrastructure
{
    public class SessionConfigurationFileHandler
    {
        public string GetFilenameProposal(string[] participants)
        {
            string configName = string.Join("_", participants);
            return configName + ".vpcsession";
        }

        public SessionConfiguration Load(string filePath)
        {
            var configFile = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<SessionConfiguration>(configFile)!;
        }

        public void Save(string filePath, SessionConfiguration serviceConfiguration)
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
