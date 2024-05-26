using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Data.Test;
public sealed class BaseTestData : IDisposable
{
    private readonly string connectionTestMySQL = @$"Persist Security Info=True;
                                                    Server=localhost;
                                                    Port=3306;
                                                    Database=dbApiTest_{Guid.NewGuid().ToString().Replace("-", "")};
                                                    Uid=root;
                                                    Pwd=root";
    public ServiceProvider ServiceProvider { get; set; }

    public BaseTestData()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<MyContext>( options =>
            options.UseMySql(connectionTestMySQL,ServerVersion.AutoDetect(connectionTestMySQL)),
            ServiceLifetime.Transient);

        ServiceProvider = serviceCollection.BuildServiceProvider();
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context!.Database.EnsureCreated();
        }
    }
    public void Dispose()
    {
        using (var context = ServiceProvider.GetService<MyContext>())
        {
            context!.Database.EnsureDeleted();
        }
    }
}
