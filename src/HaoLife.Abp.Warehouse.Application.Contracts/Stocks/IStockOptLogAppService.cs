using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作日志服务
/// </summary>
public interface IStockOptLogAppService : IApplicationService
{

    /// <summary>
    /// 获取库存操作日志
    /// </summary>
    /// <param name="input">操作日志查询</param>
    /// <returns></returns>
    Task<PagedResultDto<StockOptLogDto>> GetListAsync(StockOptLogSearchDto input);
    
    /// <summary>
    /// 查询库存操作日志
    /// </summary>
    /// <param name="stockId">库存id</param>
    /// <returns></returns>
    Task<List<StockOptLogDto>> GetListByStockIdAsync(Guid stockId);
}
