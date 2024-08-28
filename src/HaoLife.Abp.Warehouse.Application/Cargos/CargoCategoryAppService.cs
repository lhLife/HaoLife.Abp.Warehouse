using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类别服务
/// </summary>
//[RequiresFeature(WarehouseFeatures.CargoCategoryEnable)]
//[Authorize(WarehousePermissions.CargoCategory.Default)]
//[RequiresGlobalFeature(typeof(CargoCategoryFeature))]
public class CargoCategoryAppService : WarehouseAppService, ICargoCategoryAppService
{
    private readonly ICargoCategoryRepository cargoCategoryRepository;

    public CargoCategoryAppService(
        ICargoCategoryRepository cargoCategoryRepository)
    {
        this.cargoCategoryRepository = cargoCategoryRepository;
    }
    public async Task<CargoCategoryDto> CreateAsync(CargoCategoryCreateDto input)
    {
        var model = new CargoCategory(this.GuidGenerator.Create(), input.Name, input.ParentId, CurrentTenant.Id);
        input.MapExtraPropertiesTo(model);

        await this.cargoCategoryRepository.InsertAsync(model);

        return this.ObjectMapper.Map<CargoCategory, CargoCategoryDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.cargoCategoryRepository.DeleteAsync(id);
    }

    public async Task<List<CargoCategoryNodeDto>> GetAllAsync()
    {
        var all = await this.cargoCategoryRepository.GetListAsync();

        var nodes = this.ObjectMapper.Map<List<CargoCategory>, List<CargoCategoryNodeDto>>(all);
        var maps = nodes.ToDictionary(a => a.Id);
        foreach (var node in nodes)
        {
            if (!node.ParentId.HasValue) continue;

            if (maps.TryGetValue(node.ParentId.Value, out CargoCategoryNodeDto? parent))
            {
                if (parent.Childrens == null) parent.Childrens = new List<CargoCategoryNodeDto>();
                parent.Childrens.Add(node);

            }
        }

        nodes.ForEach(a => a.IsHaveChild = a.Childrens != null && a.Childrens.Count > 0);

        return nodes.Where(a => a.ParentId == null).ToList();
    }

    public async Task<List<CargoCategoryNodeDto>> GetChildrenAsync(Guid parentId)
    {
        var childs = await this.cargoCategoryRepository.GetChildsAsync(parentId);

        var nodes = this.ObjectMapper.Map<List<CargoCategory>, List<CargoCategoryNodeDto>>(childs);
        var childsCounts = await this.cargoCategoryRepository.GetChildCountAsync(childs.Select(a => a.Id).ToList());
        nodes.ForEach(node =>
        {
            node.IsHaveChild = false;
            if (childsCounts.TryGetValue(node.Id, out int count))
            {
                node.IsHaveChild = count > 0;
            }
        });

        return nodes;
    }

    public async Task<List<CargoCategoryNodeDto>> GetRootAsync()
    {
        var roots = await this.cargoCategoryRepository.GetRootsAsync();

        var nodes = this.ObjectMapper.Map<List<CargoCategory>, List<CargoCategoryNodeDto>>(roots);

        var childsCounts = await this.cargoCategoryRepository.GetChildCountAsync(roots.Select(a => a.Id).ToList());

        nodes.ForEach(node =>
        {
            node.IsHaveChild = false;
            if (childsCounts.TryGetValue(node.Id, out int count))
            {
                node.IsHaveChild = count > 0;
            }
        });


        return nodes;
    }

    public async Task<CargoCategoryDto> UpdateAsync(Guid id, CargoCategoryCreateDto input)
    {
        var model = await this.cargoCategoryRepository.GetAsync(id);
        model.SetParentId(input.ParentId);
        model.SetName(input.Name);

        input.MapExtraPropertiesTo(model);

        await this.cargoCategoryRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<CargoCategory, CargoCategoryDto>(model);
    }
}
