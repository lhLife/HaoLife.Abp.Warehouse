using HaoLife.Abp.Warehouse.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Cargos;

public interface ICargoAppService : IApplicationService
    , ICrudAppService<CargoDto, CargoListDto, Guid, CargoSearchDto, CargoCreateDto, CargoCreateDto>
{
}
