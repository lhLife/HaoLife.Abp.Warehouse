using Volo.Abp;
using Volo.Abp.MongoDB;

namespace HaoLife.Abp.Warehouse.MongoDB;

public static class WarehouseMongoDbContextExtensions
{
    public static void ConfigureWarehouse(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
