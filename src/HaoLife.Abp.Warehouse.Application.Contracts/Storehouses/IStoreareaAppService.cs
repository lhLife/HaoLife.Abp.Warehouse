using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 库区服务
/// </summary>
public interface IStoreareaAppService : IApplicationService
    , ICrudAppService<StoreareaDto, StoreareaDto, Guid, StoreareaSearchDto, StoreareaCreateDto, StoreareaCreateDto>
{
    public Task<List<StoreareaDto>> GetEnableListAsync(Guid storehouseId);
}
