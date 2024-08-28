using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类别服务
/// </summary>
public interface ICargoCategoryAppService : IApplicationService
    , ICreateUpdateAppService<CargoCategoryDto, Guid, CargoCategoryCreateDto, CargoCategoryCreateDto>
    , IDeleteAppService<Guid>
{
    /// <summary>
    /// 获取所有类别
    /// </summary>
    /// <returns></returns>
    public Task<List<CargoCategoryNodeDto>> GetAllAsync();
    /// <summary>
    /// 货物根类别
    /// </summary>
    /// <returns></returns>
    public Task<List<CargoCategoryNodeDto>> GetRootAsync();
    /// <summary>
    /// 获取子类别
    /// </summary>
    /// <param name="parentId"></param>
    /// <returns></returns>
    public Task<List<CargoCategoryNodeDto>> GetChildrenAsync(Guid parentId);
}
