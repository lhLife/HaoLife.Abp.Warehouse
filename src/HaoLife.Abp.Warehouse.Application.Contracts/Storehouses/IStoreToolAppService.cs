using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStoreToolAppService : IApplicationService
    , ICrudAppService<StoreToolDto, StoreToolDto, Guid, StoreToolSearchDto, StoreToolCreateDto, StoreToolCreateDto>
{
}
