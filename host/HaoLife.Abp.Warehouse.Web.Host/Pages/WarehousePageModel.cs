using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HaoLife.Abp.Warehouse.Pages;

public abstract class WarehousePageModel : AbpPageModel
{
    protected WarehousePageModel()
    {
        LocalizationResourceType = typeof(WarehouseResource);
    }
}
