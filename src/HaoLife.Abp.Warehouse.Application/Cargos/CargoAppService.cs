using AutoMapper.Features;
using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Localization;
using HaoLife.Abp.Warehouse.Permissions;
using HaoLife.Abp.Warehouse.Suppliers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation.StringValues;
using static System.Diagnostics.Activity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HaoLife.Abp.Warehouse.Cargos;

//[RequiresFeature(WarehouseFeatures.CargoEnable)]
//[Authorize(WarehousePermissions.Cargo.Default)]
public class CargoAppService :
    WarehouseAppService,
    ICargoAppService
{
    private readonly ICargoRepository cargoRepository;
    private readonly FeatureDefinitionManager featureDefinitionManager;
    private readonly IStaticFeatureDefinitionStore staticFeatureDefinitionStore;
    private readonly IDynamicFeatureDefinitionStore dynamicFeatureDefinitionStore;

    public CargoAppService(ICargoRepository cargoRepository, FeatureDefinitionManager featureDefinitionManager
        , IStaticFeatureDefinitionStore staticFeatureDefinitionStore
        , IDynamicFeatureDefinitionStore dynamicFeatureDefinitionStore)
    {
        this.cargoRepository = cargoRepository;
        this.featureDefinitionManager = featureDefinitionManager;
        this.staticFeatureDefinitionStore = staticFeatureDefinitionStore;
        this.dynamicFeatureDefinitionStore = dynamicFeatureDefinitionStore;
    }
    public async Task<CargoDto> CreateAsync(CargoCreateDto input)
    {
        var model = new Cargo(this.GuidGenerator.Create(), input.Name, input.Images, input.Bn, input.Sn, input.IsEnable
            , input.Weight, input.Unit, string.Join(",", (input.Specs ?? Enumerable.Empty<CargoSpecItemCreateDto>()).Select(a => $"{a.Name}:{a.Value}").ToList())
            , input.CostPrice, CurrentTenant.Id);

        if (input.CategoryId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.CargoCategoryEnable))
        {
            var cargoCategory = await this.LazyServiceProvider.LazyGetRequiredService<ICargoCategoryRepository>()
                .GetAsync(input.CategoryId.Value);

            model.SetCategoryId(cargoCategory.Id);


        }
        //供应商仓储实现
        if (input.SupplierId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.SupplierEnable))
        {
            var supplier = await this.LazyServiceProvider.LazyGetRequiredService<ISupplierRepository>()
                .GetAsync(input.SupplierId.Value);

            model.SetSupplierId(supplier.Id);
        }

        input.MapExtraPropertiesTo(model);
        await this.cargoRepository.InsertAsync(model);

        return this.ObjectMapper.Map<Cargo, CargoDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.cargoRepository.DeleteAsync(id);
    }

    public async Task<CargoDto> GetAsync(Guid id)
    {
        var model = await this.cargoRepository.GetAsync(id);

        return this.ObjectMapper.Map<Cargo, CargoDto>(model);
    }

    public async Task<PagedResultDto<CargoListDto>> GetListAsync(CargoSearchDto input)
    {

        var query = await this.cargoRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));
        query = query.WhereIf(!input.Bn.IsNullOrWhiteSpace(), a => a.Bn == input.Bn);
        query = query.WhereIf(!input.Sn.IsNullOrWhiteSpace(), a => a.Sn == input.Sn);

        if (!input.CategoryName.IsNullOrWhiteSpace()
            && GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.CargoCategoryEnable))
        {
            var categoryQuery = await this.LazyServiceProvider.LazyGetRequiredService<ICargoCategoryRepository>().GetQueryableAsync();

            query = query.GroupJoin(categoryQuery, a => a.CategoryId, a => a.Id, (a, b) => new { a, b })
                .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b })
                .Where(a => a.b!.Name.Contains(input.CategoryName))
                .Select(a => a.a);

        }

        var count = query.Count();

        query = query.OrderByIf<Cargo, IQueryable<Cargo>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);

        var ls = query.ToList();
        var result = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(ls);
        return new PagedResultDto<CargoListDto>(count, result);

    }

    public async Task<CargoDto> UpdateAsync(Guid id, CargoCreateDto input)
    {
        var model = await this.cargoRepository.GetAsync(id);
        model.SetName(input.Name);
        model.SetImages(input.Images);
        model.SetSn(input.Sn);
        model.SetBn(input.Bn);
        model.SetEnable(input.IsEnable);
        model.SetWeight(input.Weight);
        model.SetUnit(input.Unit);
        model.SetSpecDesc(string.Join(",", (input.Specs ?? Enumerable.Empty<CargoSpecItemCreateDto>()).Select(a => $"{a.Name}:{a.Value}").ToList()));
        model.SetCostPrice(input.CostPrice);

        if (input.CategoryId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.CargoCategoryEnable))
        {
            var cargoCategory = await this.LazyServiceProvider.LazyGetRequiredService<ICargoCategoryRepository>()
                .GetAsync(input.CategoryId.Value);

            model.SetCategoryId(cargoCategory.Id);


        }
        //供应商仓储实现
        if (input.SupplierId.HasValue
            && GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>()
            && await FeatureChecker.IsEnabledAsync(WarehouseFeatures.SupplierEnable))
        {
            var supplier = await this.LazyServiceProvider.LazyGetRequiredService<ISupplierRepository>()
                .GetAsync(input.SupplierId.Value);

            model.SetSupplierId(supplier.Id);
        }

        input.MapExtraPropertiesTo(model);

        await this.cargoRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<Cargo, CargoDto>(model);
    }
}
