using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var connection = "Server=.\\SQLEXPRESS2019;Initial Catalog=dbapi;MultipleActiveResultSets=true;User ID=sa;Password=root;TrustServerCertificate=True";
        var optionBuilder = new DbContextOptionsBuilder<MyContext>();
        //optionBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
        optionBuilder.UseSqlServer(connection); 
        return new MyContext(optionBuilder.Options);
    }

}
