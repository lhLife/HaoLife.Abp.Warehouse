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
[ControllerName("Storelocation")]
[Route("api/Warehouse/Storelocation")]
//[Authorize]
public class StorelocationController : AbpControllerBase, IStorelocationAppService
{
    private readonly IStorelocationAppService storelocationAppService;

    public StorelocationController(IStorelocationAppService storelocationAppService)
    {
        this.storelocationAppService = storelocationAppService;
    }

    [HttpPost]
    public Task<StorelocationDto> CreateAsync(StorelocationCreateDto input)
    {
        return this.storelocationAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.storelocationAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<StorelocationDto> GetAsync(Guid id)
    {
        return this.storelocationAppService.GetAsync(id);
    }

    [HttpGet("enable")]
    public Task<List<StorelocationDto>> GetEnableListAsync(Guid storehouseId, Guid storeareaId)
    {
        return this.storelocationAppService.GetEnableListAsync(storehouseId, storeareaId);
    }

    [HttpGet]
    public Task<PagedResultDto<StorelocationDto>> GetListAsync(StorelocationSearchDto input)
    {
        return this.storelocationAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task<StorelocationDto> UpdateAsync(Guid id, StorelocationCreateDto input)
    {
        return this.storelocationAppService.UpdateAsync(id, input);
    }
}
