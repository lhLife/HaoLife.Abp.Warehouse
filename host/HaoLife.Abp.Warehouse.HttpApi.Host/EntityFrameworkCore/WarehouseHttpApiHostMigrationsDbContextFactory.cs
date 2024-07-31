using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

public class WarehouseHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<WarehouseHttpApiHostMigrationsDbContext>
{
    public WarehouseHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<WarehouseHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Warehouse"));

        return new WarehouseHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
