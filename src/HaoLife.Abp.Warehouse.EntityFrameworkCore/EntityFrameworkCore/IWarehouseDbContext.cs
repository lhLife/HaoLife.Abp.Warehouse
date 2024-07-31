using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

[ConnectionStringName(WarehouseDbProperties.ConnectionStringName)]
public interface IWarehouseDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
