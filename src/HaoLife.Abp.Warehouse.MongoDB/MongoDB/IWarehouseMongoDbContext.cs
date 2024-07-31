using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace HaoLife.Abp.Warehouse.MongoDB;

[ConnectionStringName(WarehouseDbProperties.ConnectionStringName)]
public interface IWarehouseMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
