using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse;

public abstract class WarehouseAppService : ApplicationService
{
    protected WarehouseAppService()
    {
        LocalizationResource = typeof(WarehouseResource);
        ObjectMapperContext = typeof(WarehouseApplicationModule);
    }
}
