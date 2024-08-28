using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StoreToolAppService : WarehouseAppService, IStoreToolAppService
{
    private readonly IStoreToolRepository storeToolRepository;

    public StoreToolAppService(IStoreToolRepository storeToolRepository)
    {
        this.storeToolRepository = storeToolRepository;
    }

    public async Task<StoreToolDto> CreateAsync(StoreToolCreateDto input)
    {
        var model = new StoreTool(this.GuidGenerator.Create(), input.Name, input.Sort, CurrentTenant.Id);

        foreach (var (item, i) in input.Attrs.Select((item, i) => (item, i)))
        {
            model.AddAttr(this.GuidGenerator.Create(), item, i);
        }
        input.MapExtraPropertiesTo(model);
        await this.storeToolRepository.InsertAsync(model);

        return this.ObjectMapper.Map<StoreTool, StoreToolDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.storeToolRepository.DeleteAsync(id);
    }

    public async Task<StoreToolDto> GetAsync(Guid id)
    {
        var model = await this.storeToolRepository.GetAsync(id);

        return this.ObjectMapper.Map<StoreTool, StoreToolDto>(model);
    }

    public async Task<PagedResultDto<StoreToolDto>> GetListAsync(StoreToolSearchDto input)
    {
        var query = await this.storeToolRepository.GetQueryableAsync();

        query = query.WhereIf(!input.Name.IsNullOrWhiteSpace(), a => a.Name.Contains(input.Name!));

        var count = query.Count();

        query = query.OrderByIf<StoreTool, IQueryable<StoreTool>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<StoreTool>, List<StoreToolDto>>(ls);

        return new PagedResultDto<StoreToolDto>(count, result);
    }

    public async Task<StoreToolDto> UpdateAsync(Guid id, StoreToolCreateDto input)
    {
        var model = await this.storeToolRepository.GetAsync(id);
        model.Attrs.Clear();
        model.SetName(input.Name);
        model.SetSort(input.Sort);

        foreach (var (item, i) in input.Attrs.Select((item, i) => (item, i)))
        {
            model.AddAttr(this.GuidGenerator.Create(), item, i);
        }
        input.MapExtraPropertiesTo(model);

        return this.ObjectMapper.Map<StoreTool, StoreToolDto>(model);
    }
}
