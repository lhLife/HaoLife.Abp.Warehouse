using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseApplicationModule),
    typeof(WarehouseDomainTestModule)
    )]
public class WarehouseApplicationTestModule : AbpModule
{

}
