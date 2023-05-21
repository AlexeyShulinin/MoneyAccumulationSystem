using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MoneyAccumulationSystem.Services.Behaviours;

namespace MoneyAccumulationSystem.Services.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAndConfigureMediatr(this IServiceCollection services)
    {
        services.AddMediatR(config 
            => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    }

    public static void AddAndConfigureValidation(this IServiceCollection services)
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}