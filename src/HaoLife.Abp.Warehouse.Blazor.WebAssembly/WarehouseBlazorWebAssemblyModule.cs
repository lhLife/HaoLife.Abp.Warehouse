using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse.Blazor.WebAssembly;

[DependsOn(
    typeof(WarehouseBlazorModule),
    typeof(WarehouseHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class WarehouseBlazorWebAssemblyModule : AbpModule
{

}
