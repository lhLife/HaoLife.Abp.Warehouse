using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(WarehouseDomainSharedModule)
)]
public class WarehouseDomainModule : AbpModule
{

    public override void ConfigureServices(ServiceConfigurationContext context)
    {

        //if (GlobalFeatureManager.Instance.IsEnabled<ReactionsFeature>())
        //{

        //}
    }
}
