using Microsoft.Extensions.DependencyInjection;
using HaoLife.Abp.Warehouse.Blazor.Menus;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.UI.Navigation;

namespace HaoLife.Abp.Warehouse.Blazor;

[DependsOn(
    typeof(WarehouseApplicationContractsModule),
    typeof(AbpAspNetCoreComponentsWebThemingModule),
    typeof(AbpAutoMapperModule)
    )]
public class WarehouseBlazorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<WarehouseBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<WarehouseBlazorAutoMapperProfile>(validate: true);
        });

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new WarehouseMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(WarehouseBlazorModule).Assembly);
        });
    }
}
