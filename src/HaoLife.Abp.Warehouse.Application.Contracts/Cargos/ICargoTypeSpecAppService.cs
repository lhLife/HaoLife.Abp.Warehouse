using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类型规格服务
/// </summary>
public interface ICargoTypeSpecAppService : IApplicationService
    , ICrudAppService<CargoTypeSpecDto, CargoTypeSpecDto, Guid, CargoTypeSpecSearchDto, CargoTypeSpecCreateDto, CargoTypeSpecCreateDto>
{
}
