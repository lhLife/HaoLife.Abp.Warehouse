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

namespace HaoLife.Abp.Warehouse.Cargos;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("CargoTypeSpec")]
[Route("api/Warehouse/CargoTypeSpec")]
//[Authorize]
public class CargoTypeSpecController : AbpControllerBase, ICargoTypeSpecAppService
{
    private readonly ICargoTypeSpecAppService cargoTypeSpecAppService;

    public CargoTypeSpecController(ICargoTypeSpecAppService cargoTypeSpecAppService)
    {
        this.cargoTypeSpecAppService = cargoTypeSpecAppService;
    }

    [HttpPost]
    public Task<CargoTypeSpecDto> CreateAsync(CargoTypeSpecCreateDto input)
    {
        return this.cargoTypeSpecAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.cargoTypeSpecAppService.DeleteAsync(id);
    }

    [HttpGet("{id}")]
    public Task<CargoTypeSpecDto> GetAsync(Guid id)
    {
        return this.cargoTypeSpecAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<CargoTypeSpecDto>> GetListAsync(CargoTypeSpecSearchDto input)
    {
        return this.cargoTypeSpecAppService.GetListAsync(input);
    }

    [HttpPut("{id}")]
    public Task<CargoTypeSpecDto> UpdateAsync(Guid id, CargoTypeSpecCreateDto input)
    {
        return this.cargoTypeSpecAppService.UpdateAsync(id, input);
    }
}
