using Api.Data.Mapping;
using Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using System.Data;

namespace Api.Data.Context;
public class MyContext : DbContext
{

    public MyContext(DbContextOptions options) : base (options) {}

    public DbSet<UserEntity> Users { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=root",
                       ServerVersion.AutoDetect("Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=root"));
    }
}
