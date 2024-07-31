using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseDomainModule),
    typeof(WarehouseTestBaseModule)
)]
public class WarehouseDomainTestModule : AbpModule
{

}
