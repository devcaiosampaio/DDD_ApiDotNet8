using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var connection = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=root";
        var optionBuilder = new DbContextOptionsBuilder<MyContext>();
        optionBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
        return new MyContext(optionBuilder.Options);
    }
}
