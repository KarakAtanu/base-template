using Microsoft.Extensions.DependencyInjection;

namespace BaseTemplate.Application;

/// <summary>
/// Dependency injection configuration for the Application layer.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds application layer services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection</param>
    /// <returns>The service collection for chaining</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // TODO: Add MediatR configuration
        // services.AddMediatR(typeof(DependencyInjection).Assembly);

        // TODO: Add AutoMapper configuration
        // services.AddAutoMapper(typeof(DependencyInjection).Assembly);

        // TODO: Add FluentValidation configuration
        // services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

        return services;
    }
}
