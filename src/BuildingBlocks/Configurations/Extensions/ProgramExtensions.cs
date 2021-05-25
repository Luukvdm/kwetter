using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Kwetter.BuildingBlocks.Configurations.Extensions
{
    public static class ProgramExtension
    {
        public static IConfigurationBuilder AddSharedJson(this IConfigurationBuilder builder,
            IHostEnvironment hostEnvironment)
        {
            string sharedFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.AddJsonFile(Path.Combine(sharedFolder, "SharedSettings.json"),
                true,
                true);
            builder.AddJsonFile(
                Path.Combine(sharedFolder,
                    $"SharedSettings.{hostEnvironment.EnvironmentName}.json"),
                true, true);

            return builder;
        }
    }
}