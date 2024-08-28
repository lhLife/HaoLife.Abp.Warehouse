using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

[ConnectionStringName(WarehouseDbProperties.ConnectionStringName)]
public class WarehouseDbContext : AbpDbContext<WarehouseDbContext>, IWarehouseDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureWarehouse();

    }
}
