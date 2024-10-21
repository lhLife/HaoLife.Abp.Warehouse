using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Suppliers;

/// <summary>
/// 供应商服务
/// </summary>
public interface ISupplierAppService : IApplicationService
    , ICrudAppService<SupplierDto, SupplierDto, Guid, SupplierSearchDto, SupplierCreateDto, SupplierCreateDto>
{
}
