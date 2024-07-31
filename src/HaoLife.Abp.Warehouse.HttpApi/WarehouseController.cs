using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace HaoLife.Abp.Warehouse;

public abstract class WarehouseController : AbpControllerBase
{
    protected WarehouseController()
    {
        LocalizationResource = typeof(WarehouseResource);
    }
}
