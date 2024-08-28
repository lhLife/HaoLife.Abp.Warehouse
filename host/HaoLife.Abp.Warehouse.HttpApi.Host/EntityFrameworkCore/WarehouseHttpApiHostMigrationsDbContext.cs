using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

public class WarehouseHttpApiHostMigrationsDbContext : AbpDbContext<WarehouseHttpApiHostMigrationsDbContext>
{
    public WarehouseHttpApiHostMigrationsDbContext(DbContextOptions<WarehouseHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureWarehouse();
    }
}
