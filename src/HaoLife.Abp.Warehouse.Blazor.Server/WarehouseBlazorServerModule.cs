using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(WarehouseBlazorModule)
    )]
public class WarehouseBlazorServerModule : AbpModule
{

}
