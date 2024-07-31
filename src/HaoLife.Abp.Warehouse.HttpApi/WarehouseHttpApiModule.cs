using Localization.Resources.AbpUi;
using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class WarehouseHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(WarehouseHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<WarehouseResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
