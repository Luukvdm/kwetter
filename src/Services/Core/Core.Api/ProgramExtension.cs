using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kwetter.Services.Core.Api
{
    public static class ProgramExtension
    {
        public static IConfigurationBuilder AddSharedJson(this IConfigurationBuilder builder,
            IHostEnvironment hostEnvironment)
        {
            string sharedFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            // string sharedFolder = Path.Combine(hostEnvironment.ContentRootPath, "../..", "Core/ServiceCore");

            builder.AddJsonFile(Path.Combine(sharedFolder, "SharedSettings.json"),
                true,
                true);
            builder.AddJsonFile(
                Path.Combine(sharedFolder,
                    $"SharedSettings.{hostEnvironment.EnvironmentName}.json"),
                true, true);


            return builder;
        }

        public static IWebHostBuilder AddLogger(this IWebHostBuilder builder)
        {
            builder.UseSerilog();
            return builder;
        }

        public static IHostBuilder AddLogger(this IHostBuilder builder)
        {
            builder.UseSerilog();
            return builder;
        }
    }
}
