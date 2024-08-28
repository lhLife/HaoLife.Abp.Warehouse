using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;


//[RequiresFeature(WarehouseFeatures.StorelocationEnable)]
//[Authorize(WarehousePermissions.Storelocation.Default)]
//[RequiresGlobalFeature(typeof(StorehouseFeature))]
public class StorelocationAppService : WarehouseAppService, IStorelocationAppService
{
    private readonly IStorelocationRepository storelocationRepository;
    private readonly IStoreareaRepository storeareaRepository;
    private readonly IStorehouseRepository storehouseRepository;

    public StorelocationAppService(IStorelocationRepository storelocationRepository
        , IStoreareaRepository storeareaRepository
        , IStorehouseRepository storehouseRepository)
    {
        this.storelocationRepository = storelocationRepository;
        this.storeareaRepository = storeareaRepository;
        this.storehouseRepository = storehouseRepository;
    }
    public async Task<StorelocationDto> CreateAsync(StorelocationCreateDto input)
    {
        var storehouse = await this.storehouseRepository.GetAsync(input.StorehouseId);
        var storearea = await this.storeareaRepository.GetAsync(input.StoreareaId);

        var model = new Storelocation(this.GuidGenerator.Create(), input.Code, input.IsEnable
            , storehouse.Id, storearea.Id
            , CurrentTenant.Id);

        if (input.StoreToolId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StoreToolEnable))
        {
            var storeToolRepository = this.LazyServiceProvider.LazyGetRequiredService<IStoreToolRepository>();

            var storeTool = await storeToolRepository.GetAsync(input.StoreToolId.Value);

            if (!input.StoreToolAttrs.All(a => storeTool.Attrs.Any(b => b.Name == a.Name)))
                throw new BusinessException();

            var attrs = storeTool.Attrs.OrderBy(a => a.Sort).Select(a => input.StoreToolAttrs.FirstOrDefault(b => b.Name == a.Name)).Select(a => $"{a.Name}:{a.Value}");
            model.SetStoreToolAttrDesc(string.Join(",", attrs));
        }

        input.MapExtraPropertiesTo(model);
        await this.storelocationRepository.InsertAsync(model);

        var result = this.ObjectMapper.Map<Storelocation, StorelocationDto>(model);
        result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
        result.Storearea = this.ObjectMapper.Map<Storearea, StoreareaListDto>(storearea);
        return result;
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.storelocationRepository.DeleteAsync(id);
    }

    public async Task<StorelocationDto> GetAsync(Guid id)
    {
        var model = await this.storelocationRepository.GetAsync(id);

        var result = this.ObjectMapper.Map<Storelocation, StorelocationDto>(model);
        if (result != null)
        {
            var storehouse = await this.storehouseRepository.GetAsync(result.StorehouseId);
            var storearea = await this.storeareaRepository.GetAsync(result.StoreareaId);
            result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
            result.Storearea = this.ObjectMapper.Map<Storearea, StoreareaListDto>(storearea);
        }
        return result!;
    }


    public async Task<List<StorelocationDto>> GetEnableListAsync(Guid storehouseId, Guid storeareaId)
    {
        var ls = await this.storelocationRepository.GetListAsync(a => a.StorehouseId == storehouseId
            && a.StoreareaId == storeareaId && a.IsEnable);

        var result = this.ObjectMapper.Map<List<Storelocation>, List<StorelocationDto>>(ls);
        if (result != null && ls.Count > 0)
        {
            var hids = ls.Select(a => a.StorehouseId).Distinct().ToList();
            var aids = ls.Select(a => a.StoreareaId).Distinct().ToList();
            var housels = await this.storehouseRepository.GetListAsync(hids);
            var areals = await this.storeareaRepository.GetListAsync(aids);

            var houseMap = this.ObjectMapper.Map<List<Storehouse>, List<StorehouseListDto>>(housels).ToDictionary(a => a.Id);
            var areaMap = this.ObjectMapper.Map<List<Storearea>, List<StoreareaListDto>>(areals).ToDictionary(a => a.Id);

            foreach (var item in result)
            {
                item.Storearea = areaMap.GetOrDefault(item.StoreareaId)!;
                item.Storehouse = houseMap.GetOrDefault(item.StorehouseId)!;
            }
        }

        return result!;
    }

    public async Task<PagedResultDto<StorelocationDto>> GetListAsync(StorelocationSearchDto input)
    {
        var query = await this.storelocationRepository.GetQueryableAsync();

        query = query.WhereIf(input.StorehouseId.HasValue, a => a.StorehouseId == input.StorehouseId);
        query = query.WhereIf(!input.Code.IsNullOrWhiteSpace(), a => a.Code.Contains(input.Code!));

        if (!input.StoreareaName.IsNullOrWhiteSpace() || input.StoreareaType.HasValue)
        {
            var q1 = await this.storeareaRepository.GetQueryableAsync();
            var q2 = query.GroupJoin(q1, a => a.StoreareaId, b => b.Id, (a, b) => new { a, b })
                .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b });

            q2 = q2.WhereIf(!input.StoreareaName.IsNullOrWhiteSpace(), a => a.b!.Name.Contains(input.StoreareaName!));
            q2 = q2.WhereIf(input.StoreareaType.HasValue, a => a.b!.StoreareaType == input.StoreareaType);


            query = q2.Select(a => a.a);
        }
        if (!input.StorehouseName.IsNullOrWhiteSpace())
        {
            var q1 = await this.storehouseRepository.GetQueryableAsync();
            query = query.GroupJoin(q1, a => a.StorehouseId, b => b.Id, (a, b) => new { a, b })
                .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b })
                .Where(a => a.b!.Name.Contains(input.StorehouseName))
                .Select(a => a.a);
        }

        var count = query.Count();

        query = query.OrderByIf<Storelocation, IQueryable<Storelocation>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);


        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();


        var result = this.ObjectMapper.Map<List<Storelocation>, List<StorelocationDto>>(ls);
        if (result != null && ls.Count > 0)
        {
            var hids = ls.Select(a => a.StorehouseId).Distinct().ToList();
            var aids = ls.Select(a => a.StoreareaId).Distinct().ToList();
            var housels = await this.storehouseRepository.GetListAsync(hids);
            var areals = await this.storeareaRepository.GetListAsync(aids);

            var houseMap = this.ObjectMapper.Map<List<Storehouse>, List<StorehouseListDto>>(housels).ToDictionary(a => a.Id);
            var areaMap = this.ObjectMapper.Map<List<Storearea>, List<StoreareaListDto>>(areals).ToDictionary(a => a.Id);

            foreach (var item in result)
            {
                item.Storearea = areaMap.GetOrDefault(item.StoreareaId)!;
                item.Storehouse = houseMap.GetOrDefault(item.StorehouseId)!;
            }
        }

        return new PagedResultDto<StorelocationDto>(count, result!);
    }

    public async Task<StorelocationDto> UpdateAsync(Guid id, StorelocationCreateDto input)
    {
        var storehouse = await this.storehouseRepository.GetAsync(input.StorehouseId);
        var storearea = await this.storeareaRepository.GetAsync(input.StoreareaId);

        var model = await this.storelocationRepository.GetAsync(id);
        model.SetCode(input.Code);
        model.SetEnable(input.IsEnable);
        model.SetStorehouseId(storehouse.Id);
        model.SetStoreareaId(storearea.Id);

        if (input.StoreToolId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StoreToolEnable))
        {
            var storeToolRepository = this.LazyServiceProvider.LazyGetRequiredService<IStoreToolRepository>();

            var storeTool = await storeToolRepository.GetAsync(input.StoreToolId.Value);

            if (!input.StoreToolAttrs.All(a => storeTool.Attrs.Any(b => b.Name == a.Name)))
                throw new BusinessException();

            var attrs = storeTool.Attrs.OrderBy(a => a.Sort)
                .Select(a => input.StoreToolAttrs.FirstOrDefault(b => b.Name == a.Name))
                .Where(a => a != null)
                .Select(a => $"{a!.Name}:{a!.Value}");
            model.SetStoreToolAttrDesc(string.Join(",", attrs));
        }

        input.MapExtraPropertiesTo(model);

        await this.storelocationRepository.UpdateAsync(model);

        var result = this.ObjectMapper.Map<Storelocation, StorelocationDto>(model);
        result.Storehouse = this.ObjectMapper.Map<Storehouse, StorehouseListDto>(storehouse);
        result.Storearea = this.ObjectMapper.Map<Storearea, StoreareaListDto>(storearea);

        return result;
    }
}

