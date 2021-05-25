using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Kwetter.BuildingBlocks.KwetterLogger
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureKwetterLogger(this IServiceCollection services,
            IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return services;
        }

        public static IApplicationBuilder UseKwetterLogger(this IApplicationBuilder builder,
            IHostEnvironment hostEnvironment)
        {
            builder.UseSerilogRequestLogging();

            return builder;
        }

        public static IWebHostBuilder UseKwetterLogger(this IWebHostBuilder builder)
        {
            builder.UseSerilog();
            return builder;
        }

        public static IHostBuilder UseKwetterLogger(this IHostBuilder builder)
        {
            builder.UseSerilog();
            return builder;
        }

        public static IConfigurationBuilder AddKwetterLoggerConfiguration(this IConfigurationBuilder builder,
            IHostEnvironment hostEnvironment)
        {
            string loggerFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            builder.AddJsonFile(Path.Combine(loggerFolder, "LoggerSettings.json"),
                true,
                true);
            builder.AddJsonFile(
                Path.Combine(loggerFolder,
                    $"LoggerSettings.{hostEnvironment.EnvironmentName}.json"),
                true, true);

            return builder;
        }
    }
}