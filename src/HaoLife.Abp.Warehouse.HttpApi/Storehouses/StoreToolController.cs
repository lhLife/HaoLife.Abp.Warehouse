using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace HaoLife.Abp.Warehouse.Storehouses;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("StoreTool")]
[Route("api/Warehouse/StoreTool")]
//[Authorize]
public class StoreToolController : AbpControllerBase, IStoreToolAppService
{
    private readonly IStoreToolAppService storeToolAppService;

    public StoreToolController(IStoreToolAppService storeToolAppService)
    {
        this.storeToolAppService = storeToolAppService;
    }
    [HttpPost]
    public Task<StoreToolDto> CreateAsync(StoreToolCreateDto input)
    {
        return this.storeToolAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.storeToolAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<StoreToolDto> GetAsync(Guid id)
    {
        return this.storeToolAppService.GetAsync(id);
    }
    [HttpGet]
    public Task<PagedResultDto<StoreToolDto>> GetListAsync(StoreToolSearchDto input)
    {
        return this.storeToolAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task<StoreToolDto> UpdateAsync(Guid id, StoreToolCreateDto input)
    {
        return this.storeToolAppService.UpdateAsync(id, input);
    }
}
