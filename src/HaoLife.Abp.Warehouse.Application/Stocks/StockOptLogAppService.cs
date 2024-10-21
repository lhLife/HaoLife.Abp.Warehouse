using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Storehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.ObjectMapping;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作日志服务
/// </summary>
public class StockOptLogAppService : WarehouseAppService, IStockOptLogAppService
{
    private readonly IStockOptLogRepository stockOptLogRepository;
    private readonly ICargoRepository cargoRepository;

    public StockOptLogAppService(IStockOptLogRepository stockOptLogRepository
        , ICargoRepository cargoRepository)
    {
        this.stockOptLogRepository = stockOptLogRepository;
        this.cargoRepository = cargoRepository;
    }

    public async Task<PagedResultDto<StockOptLogDto>> GetListAsync(StockOptLogSearchDto input)
    {
        var query = await this.stockOptLogRepository.GetQueryableAsync();

        query = query.WhereIf(input.CargoId.HasValue, a => a.CargoId == input.CargoId);
        query = query.WhereIf(input.StockId.HasValue, a => a.StockId == input.StockId);

        if (!input.CargoSn.IsNullOrWhiteSpace()
            || !input.CargoName.IsNullOrWhiteSpace())
        {
            var q1 = await this.cargoRepository.GetQueryableAsync();
            var q2 = query.GroupJoin(q1, a => a.CargoId, a => a.Id, (a, b) => new { a, b })
                .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b });

            q2 = q2.WhereIf(!input.CargoSn.IsNullOrWhiteSpace(), a => a.b!.Sn == input.CargoSn);
            q2 = q2.WhereIf(!input.CargoName.IsNullOrWhiteSpace(), a => a.b!.Name.Contains(input.CargoName!));

            query = q2.Select(a => a.a);
        }

        var count = query.Count();

        query = query.OrderByIf<StockOptLog, IQueryable<StockOptLog>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);


        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var cargoIds = ls.Select(a => a.CargoId).Distinct().ToList();
        var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));

        var result = this.ObjectMapper.Map<List<StockOptLog>, List<StockOptLogDto>>(ls);

        var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
        foreach (var item in result)
        {
            item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
        }

        return new PagedResultDto<StockOptLogDto>(count, result!);
    }

    public async Task<List<StockOptLogDto>> GetListByStockIdAsync(Guid stockId)
    {
        var ls = await this.stockOptLogRepository.GetListAsync(a => a.StockId == stockId);

        var cargoIds = ls.Select(a => a.CargoId).Distinct().ToList();
        var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));

        var result = this.ObjectMapper.Map<List<StockOptLog>, List<StockOptLogDto>>(ls);

        var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
        foreach (var item in result)
        {
            item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
        }

        return result;
    }
}
