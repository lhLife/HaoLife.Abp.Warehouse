using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace HaoLife.Abp.Warehouse.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class WarehouseBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Warehouse";
}
