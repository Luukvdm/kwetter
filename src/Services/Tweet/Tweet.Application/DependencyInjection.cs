using System.Reflection;
using Kwetter.Services.Core.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.Core.Tweet.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCoreApplicationServices(Assembly.GetExecutingAssembly());
            services.AddCoreApplicationBehaviours();
            
            return services;
        }
    }
}
