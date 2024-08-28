using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStoreareaAppService : IApplicationService
    , ICrudAppService<StoreareaDto, StoreareaDto, Guid, StoreareaSearchDto, StoreareaCreateDto, StoreareaCreateDto>
{
    public Task<List<StoreareaDto>> GetEnableListAsync(Guid storehouseId);
}
