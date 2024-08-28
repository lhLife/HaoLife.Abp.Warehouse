using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("StockOptLog")]
[Route("api/Warehouse/StockOptLog")]
//[Authorize]
public class StockOptLogController : ControllerBase, IStockOptLogAppService
{
    private readonly IStockOptLogAppService stockOptLogAppService;

    public StockOptLogController(IStockOptLogAppService stockOptLogAppService)
    {
        this.stockOptLogAppService = stockOptLogAppService;
    }
    [HttpGet]
    public Task<PagedResultDto<StockOptLogDto>> GetListAsync(StockOptLogSearchDto input)
    {
        return this.stockOptLogAppService.GetListAsync(input);
    }
    [HttpGet("stock/{stockId}")]
    public Task<List<StockOptLogDto>> GetListByStockIdAsync(Guid stockId)
    {
        return this.stockOptLogAppService.GetListByStockIdAsync(stockId);
    }
}
