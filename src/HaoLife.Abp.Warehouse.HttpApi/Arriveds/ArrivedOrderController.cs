using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Arriveds;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("ArrivedOrder")]
[Route("api/Warehouse/ArrivedOrder")]
//[Authorize]
public class ArrivedOrderController : ControllerBase, IArrivedOrderAppService
{
    private readonly IArrivedOrderAppService arrivedOrderAppService;

    public ArrivedOrderController(IArrivedOrderAppService arrivedOrderAppService)
    {
        this.arrivedOrderAppService = arrivedOrderAppService;
    }

    public Task<ArrivedOrderDto> AddPickItemAsync(Guid id, List<ArrivedOrderPickDto> input)
    {
        throw new NotImplementedException();
    }

    public Task<ArrivedOrderDto> ArrivedAsync(Guid id, ArrivedOrderArrivedDto input)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<ArrivedOrderDto> CreateAsync(ArrivedOrderCreateDto input)
    {
        return this.arrivedOrderAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.arrivedOrderAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<ArrivedOrderDto> GetAsync(Guid id)
    {
        return this.arrivedOrderAppService.GetAsync(id);
    }
    [HttpGet]
    public Task<PagedResultDto<ArrivedOrderListDto>> GetListAsync(ArrivedOrderSearchDto input)
    {
        return this.arrivedOrderAppService.GetListAsync(input);
    }

    public Task<ArrivedOrderDto> PickAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<ArrivedOrderDto> StockAsync(Guid id, List<ArrivedOrderStockDto> stocks)
    {
        throw new NotImplementedException();
    }

    public Task<ArrivedOrderDto> UnloadAsync(Guid id, ArrivedOrderUnloadDto input)
    {
        throw new NotImplementedException();
    }

}
