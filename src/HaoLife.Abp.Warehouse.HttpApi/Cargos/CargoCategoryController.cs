using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace HaoLife.Abp.Warehouse.Cargos;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("CargoCategory")]
[Route("api/Warehouse/CargoCategory")]
//[Authorize]
public class CargoCategoryController : AbpControllerBase, ICargoCategoryAppService
{
    private readonly ICargoCategoryAppService cargoCategoryAppService;

    public CargoCategoryController(ICargoCategoryAppService cargoCategoryAppService)
    {
        this.cargoCategoryAppService = cargoCategoryAppService;
    }
    [HttpPost]
    public Task<CargoCategoryDto> CreateAsync(CargoCategoryCreateDto input)
    {
        return this.cargoCategoryAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.cargoCategoryAppService.DeleteAsync(id);
    }

    [HttpGet("tree")]
    public Task<List<CargoCategoryNodeDto>> GetAllAsync()
    {
        return this.cargoCategoryAppService.GetAllAsync();
    }


    [HttpGet("child")]
    public Task<List<CargoCategoryNodeDto>> GetChildrenAsync(Guid parentId)
    {
        return this.cargoCategoryAppService.GetChildrenAsync(parentId);
    }

    [HttpGet("root")]
    public Task<List<CargoCategoryNodeDto>> GetRootAsync()
    {
        return this.cargoCategoryAppService.GetRootAsync();
    }

    [HttpPut("{id}")]
    public Task<CargoCategoryDto> UpdateAsync(Guid id, CargoCategoryCreateDto input)
    {
        return this.cargoCategoryAppService.UpdateAsync(id, input);
    }
}
