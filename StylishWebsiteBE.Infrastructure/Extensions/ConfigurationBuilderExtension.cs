using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace StylishWebsiteBE.Infrastructure.Extensions {
    public static class ConfigurationBuilderExtension {
        public static IConfigurationBuilder AddOcelotConfiguration(this IConfigurationBuilder builder, IHostEnvironment hostEnvironment)
        {
            const string primaryConfigurationFile = "ocelot.json";
            const string globalConfigurationFile = "ocelot.global.json";

            var configurationPath = $"./Configurations/{hostEnvironment.EnvironmentName}";

            var configurationFiles = Directory.GetFiles(configurationPath, "msa.*.json");

            var fileConfiguration = new JObject();

            foreach (var configurationFile in configurationFiles)
            {
                var configurationJson = File.ReadAllText(configurationFile);

                var configuration = JObject.Parse(configurationJson);

                fileConfiguration.Merge(configuration);
            }

            var globalConfigurationJson = File.ReadAllText(globalConfigurationFile);

            var globalConfiguration = JObject.Parse(globalConfigurationJson);

            fileConfiguration.Merge(globalConfiguration);

            var primaryConfigurationJson = JsonConvert.SerializeObject(fileConfiguration);

            File.WriteAllText(primaryConfigurationFile, primaryConfigurationJson);

            builder.AddJsonFile(primaryConfigurationFile, false, false);

            return builder;
        }
    }
}
