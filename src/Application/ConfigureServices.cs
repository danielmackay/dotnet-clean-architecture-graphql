using CA.GraphQL.Application.Common.Behaviours;
using FluentValidation;
//using MediatR;
//using MediatR.Pipeline;
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
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}
