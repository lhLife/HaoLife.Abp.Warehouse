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

namespace HaoLife.Abp.Warehouse.Suppliers;

[RemoteService(Name = WarehouseRemoteServiceConsts.RemoteServiceName)]
[Area(WarehouseRemoteServiceConsts.ModuleName)]
[ControllerName("Supplier")]
[Route("api/Warehouse/Supplier")]
//[Authorize]
public class SupplierController : AbpControllerBase, ISupplierAppService
{
    private readonly ISupplierAppService supplierAppService;

    public SupplierController(ISupplierAppService supplierAppService)
    {
        this.supplierAppService = supplierAppService;
    }
    [HttpPost]
    public Task<SupplierDto> CreateAsync(SupplierCreateDto input)
    {
        return this.supplierAppService.CreateAsync(input);
    }

    [HttpDelete("{id}")]
    public Task DeleteAsync(Guid id)
    {
        return this.supplierAppService.DeleteAsync(id);
    }

    [HttpPost("{id}")]
    public Task<SupplierDto> GetAsync(Guid id)
    {
        return this.supplierAppService.GetAsync(id);
    }

    [HttpGet]
    public Task<PagedResultDto<SupplierDto>> GetListAsync(SupplierSearchDto input)
    {
        return this.supplierAppService.GetListAsync(input);
    }
    [HttpPut("{id}")]
    public Task<SupplierDto> UpdateAsync(Guid id, SupplierCreateDto input)
    {
        return this.supplierAppService.UpdateAsync(id, input);
    }
}
