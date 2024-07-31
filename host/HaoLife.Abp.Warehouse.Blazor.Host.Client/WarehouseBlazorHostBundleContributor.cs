using Volo.Abp.Bundling;

namespace HaoLife.Abp.Warehouse.Blazor.Host.Client;

public class WarehouseBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
