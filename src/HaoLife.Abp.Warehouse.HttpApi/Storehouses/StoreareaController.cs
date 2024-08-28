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
[ControllerName("Storearea")]
[Route("api/Warehouse/Storearea")]
//[Authorize]
public class StoreareaController : AbpControllerBase, IStoreareaAppService
{
    private readonly IStoreareaAppService storeareaAppService;

    public StoreareaController(IStoreareaAppService storeareaAppService)
    {
        this.storeareaAppService = storeareaAppService;
    }
    [HttpPost]
    public Task<StoreareaDto> CreateAsync(StoreareaCreateDto input)
    {
        return this.storeareaAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.storeareaAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<StoreareaDto> GetAsync(Guid id)
    {
        return this.storeareaAppService.GetAsync(id);
    }

    [HttpGet("enable")]
    public Task<List<StoreareaDto>> GetEnableListAsync(Guid storehouseId)
    {
        return this.storeareaAppService.GetEnableListAsync(storehouseId);
    }
    [HttpGet]
    public Task<PagedResultDto<StoreareaDto>> GetListAsync(StoreareaSearchDto input)
    {
        return this.storeareaAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task<StoreareaDto> UpdateAsync(Guid id, StoreareaCreateDto input)
    {
        return this.storeareaAppService.UpdateAsync(id, input);
    }
}
