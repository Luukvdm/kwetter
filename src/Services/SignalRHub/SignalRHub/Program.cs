using System.Threading.Tasks;
using Kwetter.BuildingBlocks.Configurations.Extensions;
using Kwetter.BuildingBlocks.KwetterLogger;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.SignalRHub.SignalRHub
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
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
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}