using System.Collections.Generic;
using System.Reflection;
using FluentValidation;
using Kwetter.Services.Core.Application.Common.Behaviours;
using Kwetter.Services.Core.Application.Common.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreApplicationBehaviours(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }

        public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services,
            Assembly executingAssembly)
        {
            // Add current assembly to assembly array
            var assemblies = new[] {executingAssembly, Assembly.GetExecutingAssembly()};
            return AddCoreApplicationServices(services, executingAssembly, assemblies);
        }

        public static IServiceCollection AddCoreApplicationServices(this IServiceCollection services,
            Assembly executingAssembly,
            Assembly[] assemblies)
        {
            services.AddAutoMapper(e => e.AddProfile(new MappingProfile(executingAssembly)), executingAssembly);
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddMediatR(assemblies);

            return services;
        }
    }
}