using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类型规格服务
/// </summary>
//[RequiresFeature(WarehouseFeatures.CargoTypeSpecEnable)]
//[Authorize(WarehousePermissions.CargoTypeSpec.Default)]
//[RequiresGlobalFeature(typeof(CargoCategoryFeature))]
public class CargoTypeSpecAppService : WarehouseAppService, ICargoTypeSpecAppService
{
    private readonly ICargoTypeSpecRepository cargoTypeSpecRepository;

    public CargoTypeSpecAppService(ICargoTypeSpecRepository cargoTypeSpecRepository)
    {
        this.cargoTypeSpecRepository = cargoTypeSpecRepository;
    }

    public async Task<CargoTypeSpecDto> CreateAsync(CargoTypeSpecCreateDto input)
    {
        var model = new CargoTypeSpec(this.GuidGenerator.Create(), input.Name, input.Sort, CurrentTenant.Id);

        foreach (var (item, i) in input.Values.Select((item, i) => (item, i)))
        {
            model.AddValue(this.GuidGenerator.Create(), item, i);
        }
        input.MapExtraPropertiesTo(model);
        await this.cargoTypeSpecRepository.InsertAsync(model);

        return this.ObjectMapper.Map<CargoTypeSpec, CargoTypeSpecDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.cargoTypeSpecRepository.DeleteAsync(id);
    }

    public async Task<CargoTypeSpecDto> GetAsync(Guid id)
    {
        var model = await this.cargoTypeSpecRepository.GetAsync(id);

        return this.ObjectMapper.Map<CargoTypeSpec, CargoTypeSpecDto>(model);
    }

    public async Task<PagedResultDto<CargoTypeSpecDto>> GetListAsync(CargoTypeSpecSearchDto input)
    {
        var query = await this.cargoTypeSpecRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));

        var count = query.Count();

        query = query.OrderByIf<CargoTypeSpec, IQueryable<CargoTypeSpec>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<CargoTypeSpec>, List<CargoTypeSpecDto>>(ls);

        return new PagedResultDto<CargoTypeSpecDto>(count, result);
    }

    public async Task<CargoTypeSpecDto> UpdateAsync(Guid id, CargoTypeSpecCreateDto input)
    {
        var model = await this.cargoTypeSpecRepository.GetAsync(id);

        model.Values.Clear();
        model.SetName(input.Name);
        model.SetSort(input.Sort);

        foreach (var (item, i) in input.Values.Select((item, i) => (item, i)))
        {
            model.AddValue(this.GuidGenerator.Create(), item, i);
        }

        input.MapExtraPropertiesTo(model);

        await this.cargoTypeSpecRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<CargoTypeSpec, CargoTypeSpecDto>(model);
    }
}
