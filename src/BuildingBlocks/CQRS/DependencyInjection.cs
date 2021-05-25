using System.Reflection;
using FluentValidation;
using Kwetter.BuildingBlocks.CQRS.Behaviours;
using Kwetter.BuildingBlocks.CQRS.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.BuildingBlocks.CQRS
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCqrsServices(this IServiceCollection services, Assembly executingAssembly)
        {
            // Add current assembly to assembly array
            var assemblies = new[] {executingAssembly, Assembly.GetExecutingAssembly()};
            return AddCqrsServices(services, executingAssembly, assemblies);
        }

        public static IServiceCollection AddCqrsServices(this IServiceCollection services,
            Assembly executingAssembly,
            Assembly[] assemblies)
        {
            services.AddAutoMapper(e => e.AddProfile(new MappingProfile(executingAssembly)), executingAssembly);
            services.AddValidatorsFromAssemblies(assemblies);
            services.AddMediatR(assemblies);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            return services;
        }
    }
}