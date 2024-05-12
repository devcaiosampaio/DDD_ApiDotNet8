using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.AppDependencies;

public static class ServiceInjection
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
