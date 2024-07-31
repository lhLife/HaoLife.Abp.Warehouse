using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace HaoLife.Abp.Warehouse.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class WarehousePageModel : AbpPageModel
{
    protected WarehousePageModel()
    {
        LocalizationResourceType = typeof(WarehouseResource);
        ObjectMapperContext = typeof(WarehouseWebModule);
    }
}
