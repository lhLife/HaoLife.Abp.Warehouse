using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.Suppliers;

/// <summary>
/// 供应商服务
/// </summary>
//[RequiresFeature(WarehouseFeatures.SupplierEnable)]
//[Authorize(WarehousePermissions.Supplier.Default)]
//[RequiresGlobalFeature(typeof(SupplierFeature))]
public class SupplierAppService :
    WarehouseAppService,
    ISupplierAppService
{
    private readonly ISupplierRepository supplierRepository;

    public SupplierAppService(ISupplierRepository supplierRepository)
    {
        this.supplierRepository = supplierRepository;
    }
    public async Task<SupplierDto> CreateAsync(SupplierCreateDto input)
    {
        var model = new Supplier(this.GuidGenerator.Create()
            , input.Name
            , input.Adcode
            , input.City
            , input.FullAddress
            , input.Email
            , input.Phone
            , input.Liaisons
            , CurrentTenant.Id);


        await this.supplierRepository.InsertAsync(model);

        return this.ObjectMapper.Map<Supplier, SupplierDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.supplierRepository.DeleteAsync(id);
    }

    public async Task<SupplierDto> GetAsync(Guid id)
    {
        var model = await this.supplierRepository.GetAsync(id);

        return this.ObjectMapper.Map<Supplier, SupplierDto>(model);

    }

    public async Task<PagedResultDto<SupplierDto>> GetListAsync(SupplierSearchDto input)
    {
        var query = await this.supplierRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));
        query = query.WhereIf(!input.Phone.IsNullOrWhiteSpace(), a => a.Phone.Contains(input.Phone!));
        query = query.WhereIf(!input.Liaisons.IsNullOrWhiteSpace(), a => a.Liaisons.Contains(input.Liaisons!));

        var count = query.Count();

        query = query.OrderByIf<Supplier, IQueryable<Supplier>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<Supplier>, List<SupplierDto>>(ls);

        return new PagedResultDto<SupplierDto>(count, result);
    }

    public async Task<SupplierDto> UpdateAsync(Guid id, SupplierCreateDto input)
    {
        var model = await this.supplierRepository.GetAsync(id);
        model.SetName(input.Name);
        model.SetAdcode(input.Adcode);
        model.SetCity(input.City);
        model.SetFullAddress(input.FullAddress);
        model.SetEmail(input.Email);
        model.SetPhone(input.Phone);
        model.SetLiaisons(input.Liaisons);

        return this.ObjectMapper.Map<Supplier, SupplierDto>(model);
    }
}
