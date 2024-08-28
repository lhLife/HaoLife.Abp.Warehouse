using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStorelocationAppService : IApplicationService
    , ICrudAppService<StorelocationDto, StorelocationDto, Guid, StorelocationSearchDto, StorelocationCreateDto, StorelocationCreateDto>
{
    public Task<List<StorelocationDto>> GetEnableListAsync(Guid storehouseId, Guid storeareaId);
}
