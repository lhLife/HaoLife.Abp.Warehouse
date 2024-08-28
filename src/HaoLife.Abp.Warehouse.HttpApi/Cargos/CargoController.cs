using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace HaoLife.Abp.Warehouse.Cargos;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("Cargo")]
[Route("api/Warehouse/Cargo")]
//[Authorize]
public class CargoController : AbpControllerBase, ICargoAppService
{
    private readonly ICargoAppService cargoAppService;

    public CargoController(ICargoAppService cargoAppService)
    {
        this.cargoAppService = cargoAppService;
    }

    [HttpPost]
    public Task<CargoDto> CreateAsync(CargoCreateDto input)
    {
        return this.cargoAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.cargoAppService.DeleteAsync(id);
    }
    [HttpGet("{id}")]
    public Task<CargoDto> GetAsync(Guid id)
    {
        return this.cargoAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<CargoListDto>> GetListAsync([FromQuery]CargoSearchDto input)
    {
        return this.cargoAppService.GetListAsync(input);
    }
    [HttpPut("{id}")]
    public Task<CargoDto> UpdateAsync(Guid id, CargoCreateDto input)
    {
        return this.cargoAppService.UpdateAsync(id, input);
    }
}
