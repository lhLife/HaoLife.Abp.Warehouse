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
[ControllerName("Storehouse")]
[Route("api/Warehouse/Storehouse")]
//[Authorize]
public class StorehouseController : AbpControllerBase, IStorehouseAppService
{
    private readonly IStorehouseAppService storehouseAppService;

    public StorehouseController(IStorehouseAppService storehouseAppService)
    {
        this.storehouseAppService = storehouseAppService;
    }
    [HttpPost]
    public Task<StorehouseDto> CreateAsync(StorehouseCreateDto input)
    {
        return this.storehouseAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.storehouseAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<StorehouseDto> GetAsync(Guid id)
    {
        return this.storehouseAppService.GetAsync(id);
    }

    [HttpGet("enable")]
    public Task<List<StorehouseDto>> GetEnableListAsync()
    {
        return this.storehouseAppService.GetEnableListAsync();
    }

    [HttpGet]
    public Task<PagedResultDto<StorehouseDto>> GetListAsync(StorehouseSearchDto input)
    {
        return this.storehouseAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task<StorehouseDto> UpdateAsync(Guid id, StorehouseCreateDto input)
    {
        return this.storehouseAppService.UpdateAsync(id, input);
    }
}
