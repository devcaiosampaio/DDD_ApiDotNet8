using Api.Domain.Interfaces.User.Services;
using Api.Domain.Security;
using Api.Service.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Api.CrossCutting.AppDependencies;

public static class ServiceInjection
{
    public static IServiceCollection AddService(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ILoginService, LoginService>();
        return services;
    }
}
