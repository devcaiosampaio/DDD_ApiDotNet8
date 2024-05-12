
using Api.Data.Context;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.Services.User;
using Api.Service.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Api.CrossCutting.AppDependencies;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("dbConnection") ?? string.Empty;

        services.AddDbContext<MyContext>(options =>
              options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

        return services;

    }
}
