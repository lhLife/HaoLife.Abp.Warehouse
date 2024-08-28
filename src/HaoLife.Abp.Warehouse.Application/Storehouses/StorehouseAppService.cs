using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;

//[RequiresFeature(WarehouseFeatures.StorehouseEnable)]
//[Authorize(WarehousePermissions.Storehouse.Default)]
//[RequiresGlobalFeature(typeof(StorehouseFeature))]
public class StorehouseAppService : WarehouseAppService, IStorehouseAppService
{
    private readonly IStorehouseRepository storehouseRepository;

    public StorehouseAppService(IStorehouseRepository storehouseRepository)
    {
        this.storehouseRepository = storehouseRepository;
    }
    public async Task<StorehouseDto> CreateAsync(StorehouseCreateDto input)
    {
        var model = new Storehouse(this.GuidGenerator.Create(), input.Name, input.Adcode, input.City
            , input.FullAddress, input.Email, input.Phone, input.Liaisons, input.IsEnable
            , CurrentTenant.Id);

        input.MapExtraPropertiesTo(model);
        await this.storehouseRepository.InsertAsync(model);

        return this.ObjectMapper.Map<Storehouse, StorehouseDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.storehouseRepository.DeleteAsync(id);
    }

    public async Task<StorehouseDto> GetAsync(Guid id)
    {
        var model = await this.storehouseRepository.GetAsync(id);

        return this.ObjectMapper.Map<Storehouse, StorehouseDto>(model);
    }

    public async Task<List<StorehouseDto>> GetEnableListAsync()
    {
        var ls = await this.storehouseRepository.GetListAsync(a => a.IsEnable);

        return this.ObjectMapper.Map<List<Storehouse>, List<StorehouseDto>>(ls);
    }

    public async Task<PagedResultDto<StorehouseDto>> GetListAsync(StorehouseSearchDto input)
    {
        var query = await this.storehouseRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));
        query = query.WhereIf(!input.Adcode.IsNullOrWhiteSpace(), a => a.Adcode == input.Adcode);
        query = query.WhereIf(!input.City.IsNullOrWhiteSpace(), a => a.City.Contains(input.City!));
        query = query.WhereIf(!input.Phone.IsNullOrWhiteSpace(), a => a.Phone.Contains(input.Phone!));
        query = query.WhereIf(!input.Liaisons.IsNullOrWhiteSpace(), a => a.Liaisons.Contains(input.Liaisons!));

        var count = query.Count();

        query = query.OrderByIf<Storehouse, IQueryable<Storehouse>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<Storehouse>, List<StorehouseDto>>(ls);

        return new PagedResultDto<StorehouseDto>(count, result);
    }

    public async Task<StorehouseDto> UpdateAsync(Guid id, StorehouseCreateDto input)
    {
        var model = await this.storehouseRepository.GetAsync(id);

        model.SetName(input.Name);
        model.SetAdcode(input.Adcode);
        model.SetCity(input.City);
        model.SetFullAddress(input.FullAddress);
        model.SetEmail(input.Email);
        model.SetPhone(input.Phone);
        model.SetLiaisons(input.Liaisons);
        model.SetEnable(input.IsEnable);
        input.MapExtraPropertiesTo(model);

        await this.storehouseRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<Storehouse, StorehouseDto>(model);
    }
}
