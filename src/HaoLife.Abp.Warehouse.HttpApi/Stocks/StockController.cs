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
[ControllerName("Stock")]
[Route("api/Warehouse/Stock")]
//[Authorize]
public class StockController : ControllerBase, IStockAppService
{
    private readonly IStockAppService stockAppService;

    public StockController(IStockAppService stockAppService)
    {
        this.stockAppService = stockAppService;
    }

    [HttpPost]
    public Task<StockDto> AddAsync(StockInvokeDto input)
    {
        return this.stockAppService.AddAsync(input);
    }

    [HttpPost("batch")]
    public Task<List<StockDto>> AddBatchAsync(List<StockInvokeDto> input)
    {
        return this.stockAppService.AddBatchAsync(input);
    }

    [HttpPut]
    public Task<StockDto> DeductAsync(StockInvokeDto input)
    {
        return this.stockAppService.DeductAsync(input);
    }

    [HttpPut("batch")]
    public Task<List<StockDto>> DeductBatchAsync(List<StockInvokeDto> input)
    {
        return this.stockAppService.DeductBatchAsync(input);
    }

    [HttpPut("freeze/{id}")]
    public Task<StockDto> FreezeAsync(Guid id)
    {
        return this.stockAppService.FreezeAsync(id);
    }

    [HttpPut("freeze")]
    public Task<List<StockDto>> FreezeBatchAsync(List<Guid> ids)
    {
        return this.stockAppService.FreezeBatchAsync(ids);
    }
    [HttpGet("cargo")]
    public Task<PagedResultDto<CargoStockDto>> GetCargoStockPagedListAsync(CargoStockSearchDto input)
    {
        return this.stockAppService.GetCargoStockPagedListAsync(input);
    }
    [HttpGet]
    public Task<PagedResultDto<StockDto>> GetStackPagedListAsync(StockSearchDto input)
    {
        return this.stockAppService.GetStackPagedListAsync(input);
    }

    [HttpPut("unfreeze/{id}")]
    public Task<StockDto> UnFreezeAsync(Guid id)
    {
        return this.stockAppService.UnFreezeAsync(id);
    }

    [HttpPut("unfreeze")]
    public Task<List<StockDto>> UnFreezeBatchAsync(List<Guid> ids)
    {
        return this.stockAppService.UnFreezeBatchAsync(ids);
    }
}
