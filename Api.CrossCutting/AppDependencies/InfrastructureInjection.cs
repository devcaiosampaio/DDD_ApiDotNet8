
using Api.Data;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Data.Settings;
using Api.Domain.Interfaces;
using Api.Domain.Interfaces.User.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;

namespace Api.CrossCutting.AppDependencies;

public static class InfrastructureInjection
{
    public static IServiceCollection AddInfrastructure(
                  this IServiceCollection services,
                  IConfiguration configuration)
    {
        //Repositorios
        services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUserRepository, UserImplementation>();

        //DI Database Settings
        var configDbSettings = configuration.GetSection(nameof(DatabaseSettings));
        services.Configure<DatabaseSettings>(configDbSettings);
        DatabaseSettings databaseSetting = GetDatabaseSettingsFromConfiguration(configDbSettings);
        ValidateDatabaseSttings(databaseSetting);

        EnumDatabaseType databaseType;
        Enum.TryParse(databaseSetting?.DatabaseType, out databaseType);
        services.AddDbContext<MyContext>(options =>
                _ = databaseType switch
                {
                    EnumDatabaseType.SQLServer => options
                        .UseSqlServer(databaseSetting!.Connection),
                    EnumDatabaseType.MySql => options
                        .UseMySql(databaseSetting!.Connection,
                            ServerVersion.AutoDetect(databaseSetting.Connection)),
                    _ => throw new NotImplementedException($"O tipo da database {databaseSetting!.DatabaseType} não é suportado")
                });

        return services;

        static DatabaseSettings GetDatabaseSettingsFromConfiguration(IConfigurationSection configuration)
        {
            var databaseSetting = configuration
                .GetSection(nameof(DatabaseSettings))
                .Get<DatabaseSettings>()
                ?? throw new NotImplementedException("É necessário implementar DI DatabaseSettings conforme a documentação");
            return databaseSetting;
        }

        static void ValidateDatabaseSttings(DatabaseSettings databaseSetting)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(databaseSetting);
            if (!Validator.TryValidateObject(databaseSetting, validationContext, validationResults, true))
            {
                // Se houver erros de validação, lançar uma exceção ou tratar conforme necessário
                throw new ValidationException($"Houve erros de validação: {string.Join(", ", validationResults.Select(r => r.ErrorMessage))}");
            }
        }
    }
}
