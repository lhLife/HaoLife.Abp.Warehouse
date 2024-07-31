using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace HaoLife.Abp.Warehouse;

[Dependency(ReplaceServices = true)]
public class WarehouseBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Warehouse";
}
