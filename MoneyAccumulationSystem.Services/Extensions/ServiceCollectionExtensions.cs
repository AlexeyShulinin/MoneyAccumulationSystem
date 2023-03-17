using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

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
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}