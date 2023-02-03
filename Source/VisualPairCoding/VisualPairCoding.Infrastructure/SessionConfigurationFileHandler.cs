using System.Text.Json;

namespace VisualPairCoding.Infrastructure
{
    public class SessionConfigurationFileHandler
    {
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