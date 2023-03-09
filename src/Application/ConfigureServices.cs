using CA.GraphQL.Application.Common.Behaviours;
using CA.GraphQL.Application.Common.Interfaces;
using CA.GraphQL.Application.WeatherForecasts.Queries.GetWeatherForecasts;
using FluentValidation;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var thisAssembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(thisAssembly);
        services.AddValidatorsFromAssembly(thisAssembly);
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(thisAssembly);
            cfg.AddOpenBehavior(typeof(UnhandledExceptionBehaviour<,>));
            //cfg.AddOpenBehavior(typeof(AuthorizationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehaviour<,>));
        });

        services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();

        return services;
    }
}
