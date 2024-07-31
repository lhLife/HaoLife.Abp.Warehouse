using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.AspNetCore.Components;

namespace HaoLife.Abp.Warehouse.Blazor.Server.Host;

public abstract class WarehouseComponentBase : AbpComponentBase
{
    protected WarehouseComponentBase()
    {
        LocalizationResource = typeof(WarehouseResource);
    }
}
