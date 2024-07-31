using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class WarehouseHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(WarehouseApplicationContractsModule).Assembly,
            WarehouseRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WarehouseHttpApiClientModule>();
        });

    }
}
