using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class WarehouseInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<WarehouseInstallerModule>();
        });
    }
}
