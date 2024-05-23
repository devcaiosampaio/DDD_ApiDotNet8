
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.AppDependencies;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {
        var mySqlConnection = configuration.GetConnectionString("dbConnection") ?? string.Empty;

        services.AddDbContext<MyContext>(options =>
                //options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection))
                options.UseSqlServer(mySqlConnection)
              );

        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserImplementation>();

        return services;

    }
}
