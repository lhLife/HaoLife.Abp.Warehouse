using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;
using Volo.Abp.ObjectMapping;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 库区服务
/// </summary>
//[RequiresFeature(WarehouseFeatures.StoreareaEnable)]
//[Authorize(WarehousePermissions.Storearea.Default)]
//[RequiresGlobalFeature(typeof(StorehouseFeature))]
public class StoreareaAppService : WarehouseAppService, IStoreareaAppService
{
    private readonly IStoreareaRepository storeareaRepository;
    private readonly IStorehouseRepository storehouseRepository;

    public StoreareaAppService(IStoreareaRepository storeareaRepository
        , IStorehouseRepository storehouseRepository)
    {
        this.storeareaRepository = storeareaRepository;
        this.storehouseRepository = storehouseRepository;
    }
    public async Task<StoreareaDto> CreateAsync(StoreareaCreateDto input)
    {
        var storehouse = await this.storehouseRepository.GetAsync(input.StorehouseId);
        var model = new Storearea(this.GuidGenerator.Create(), input.Name
            , input.StoreareaType, input.IsEnable, input.StorehouseId, CurrentTenant.Id);

        input.MapExtraPropertiesTo(model);

        await this.storeareaRepository.InsertAsync(model);

        var result = this.ObjectMapper.Map<Storearea, StoreareaDto>(model);

        result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.storeareaRepository.DeleteAsync(id);
    }

    public async Task<StoreareaDto> GetAsync(Guid id)
    {
        var model = await this.storeareaRepository.GetAsync(id);
        var result = this.ObjectMapper.Map<Storearea, StoreareaDto>(model);
        if (result != null)
        {
            var storehouse = await this.storehouseRepository.GetAsync(model.StorehouseId);
            result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
        }
        return result!;
    }

    public async Task<List<StoreareaDto>> GetEnableListAsync(Guid storehouseId)
    {
        var ls = await this.storeareaRepository.GetListAsync(a => a.StorehouseId == storehouseId && a.IsEnable);

        var result = this.ObjectMapper.Map<List<Storearea>, List<StoreareaDto>>(ls);
        if (result != null && result.Count > 0)
        {
            var hids = ls.Select(a => a.StorehouseId).Distinct().ToList();
            var housels = await this.storehouseRepository.GetListAsync(hids);

            var houseMap = this.ObjectMapper.Map<List<Storehouse>, List<StorehouseListDto>>(housels).ToDictionary(a => a.Id);

            foreach (var item in result)
            {
                item.Storehouse = houseMap.GetOrDefault(item.StorehouseId)!;
            }
        }
        return result!;
    }

    public async Task<PagedResultDto<StoreareaDto>> GetListAsync(StoreareaSearchDto input)
    {
        var query = await this.storeareaRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));
        query = query.WhereIf(input.StoreareaType.HasValue, a => a.StoreareaType == input.StoreareaType);
        query = query.WhereIf(input.StorehouseId.HasValue, a => a.StorehouseId == input.StorehouseId);

        if (!input.StorehouseName.IsNullOrWhiteSpace())
        {
            var q1 = await this.storehouseRepository.GetQueryableAsync();
            query = query.GroupJoin(q1, a => a.StorehouseId, b => b.Id, (a, b) => new { a, b })
                .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b })
                .Where(a => a.b!.Name.Contains(input.StorehouseName))
                .Select(a => a.a);
        }

        var count = query.Count();

        query = query.OrderByIf<Storearea, IQueryable<Storearea>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<Storearea>, List<StoreareaDto>>(ls);
        if (result != null && result.Count > 0)
        {
            var hids = ls.Select(a => a.StorehouseId).Distinct().ToList();
            var housels = await this.storehouseRepository.GetListAsync(hids);

            var houseMap = this.ObjectMapper.Map<List<Storehouse>, List<StorehouseListDto>>(housels).ToDictionary(a => a.Id);

            foreach (var item in result)
            {
                item.Storehouse = houseMap.GetOrDefault(item.StorehouseId)!;
            }
        }
        return new PagedResultDto<StoreareaDto>(count, result!);
    }

    public async Task<StoreareaDto> UpdateAsync(Guid id, StoreareaCreateDto input)
    {
        var storehouse = await this.storehouseRepository.GetAsync(input.StorehouseId);
        var model = await this.storeareaRepository.GetAsync(id);
        model.SetName(input.Name);
        model.SetStoreareaType(input.StoreareaType);
        model.SetEnable(input.IsEnable);

        model.SetStorehouseId(storehouse.Id);
        input.MapExtraPropertiesTo(model);

        await this.storeareaRepository.UpdateAsync(model);

        var result = this.ObjectMapper.Map<Storearea, StoreareaDto>(model);

        result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
        return result;
    }
}
