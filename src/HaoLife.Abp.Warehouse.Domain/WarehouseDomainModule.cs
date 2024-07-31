using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(WarehouseDomainSharedModule)
)]
public class WarehouseDomainModule : AbpModule
{

}
