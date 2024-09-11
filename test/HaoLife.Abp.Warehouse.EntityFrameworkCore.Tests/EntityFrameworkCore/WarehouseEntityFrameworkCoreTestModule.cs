using HaoLife.Abp.Warehouse.Arriveds;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

[DependsOn(
    typeof(WarehouseApplicationTestModule),
    typeof(WarehouseEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqliteModule)
)]
public class WarehouseEntityFrameworkCoreTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAlwaysDisableUnitOfWorkTransaction();

        var sqliteConnection = CreateDatabaseAndGetConnection();

        Configure<AbpDbContextOptions>(options =>
        {
            //options.Configure(abpDbContextConfigurationContext =>
            //{
            //    abpDbContextConfigurationContext.DbContextOptions.UseSqlite(sqliteConnection);
            //});

            options.Configure<WarehouseDbContext>(opts =>
            {
                opts.DbContextOptions.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                opts.UseSqlite(sqliteConnection);
            });
        });

    }

    private static SqliteConnection CreateDatabaseAndGetConnection()
    {
        var connection = new AbpUnitTestSqliteConnection("Data Source=:memory:");
        connection.Open();

        new WarehouseDbContext(
            new DbContextOptionsBuilder<WarehouseDbContext>().UseSqlite(connection).Options
        ).GetService<IRelationalDatabaseCreator>().CreateTables();

        return connection;
    }

}
