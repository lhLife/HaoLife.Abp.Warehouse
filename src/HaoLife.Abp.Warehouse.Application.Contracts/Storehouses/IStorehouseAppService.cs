using HaoLife.Abp.Warehouse.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 仓库服务
/// </summary>
public interface IStorehouseAppService: IApplicationService
    , ICrudAppService<StorehouseDto, StorehouseDto, Guid, StorehouseSearchDto, StorehouseCreateDto, StorehouseCreateDto>
{
    public Task<List<StorehouseDto>> GetEnableListAsync();
}
