using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.KwetterLogger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Kwetter.ApiGateways.WebSpa.Aggregator
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseKwetterLogger()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddSharedJson(hostingContext.HostingEnvironment);
                    config.AddKwetterLoggerConfiguration(hostingContext.HostingEnvironment);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}