using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

/* Inherit from this class for your domain layer tests.
 * See SampleManager_Tests for example.
 */
public abstract class WarehouseDomainTestBase<TStartupModule> : WarehouseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
