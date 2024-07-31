using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace HaoLife.Abp.Warehouse.Blazor.Menus;

public class WarehouseMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(WarehouseMenus.Prefix, displayName: "Warehouse", "/Warehouse", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
