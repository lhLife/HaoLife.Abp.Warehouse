using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class WarehouseApplicationTestBase<TStartupModule> : WarehouseTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
