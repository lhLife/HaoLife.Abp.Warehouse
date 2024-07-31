using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class WarehouseApplicationContractsModule : AbpModule
{

}
