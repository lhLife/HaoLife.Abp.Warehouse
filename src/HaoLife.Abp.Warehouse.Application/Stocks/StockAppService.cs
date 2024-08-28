using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Storehouses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectMapping;

namespace HaoLife.Abp.Warehouse.Stocks;

public class StockAppService : WarehouseAppService, IStockAppService
{
    private readonly IStockRepository stockRepository;
    private readonly IStockOptLogRepository stockOptLogRepository;
    private readonly ICargoRepository cargoRepository;
    private readonly StockManager stockManager;

    public StockAppService(IStockRepository stockRepository
        , IStockOptLogRepository stockOptLogRepository
        , ICargoRepository cargoRepository
        , StockManager stockManager)
    {
        this.stockRepository = stockRepository;
        this.stockOptLogRepository = stockOptLogRepository;
        this.cargoRepository = cargoRepository;
        this.stockManager = stockManager;
    }

    public async Task<StockDto> AddAsync(StockInvokeDto input)
    {
        var cargo = await this.cargoRepository.GetAsync(input.CargoId);

        HaoLife.Abp.Warehouse.Storehouses.Storelocation storelocation = null;
        //判断是否开启仓库配置
        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            if (!await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StorehouseEnable))
                throw new BusinessException();
            if (!input.StorelocationId.HasValue)
                throw new BusinessException();

            storelocation = await this.LazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().GetAsync(input.StorelocationId.Value);
        }

        var stock = await this.stockRepository.FirstOrDefaultAsync(a => a.CargoId == input.CargoId
            && !a.IsFreeze
            && a.StorelocationId == input.StorelocationId
            && a.SeriesNumber == input.SeriesNumber
            && a.BatchNo == input.BatchNo);

        stock = await stockManager.AddAsync(stock, cargo, storelocation, input.Number, input.SeriesNumber, input.BatchNo, out bool isAdd);

        var log = new StockOptLog(this.GuidGenerator.Create(), stock, input.Number, stock.Number - input.Number, stock.Number, StockOptType.In, this.CurrentTenant.Id);

        if (isAdd)
            await this.stockRepository.InsertAsync(stock);
        else
            await this.stockRepository.UpdateAsync(stock);

        await this.stockOptLogRepository.InsertAsync(log);

        var result = this.ObjectMapper.Map<Stock, StockDto>(stock);

        result.Cargo = this.ObjectMapper.Map<Cargo, CargoListDto>(cargo);

        return result;
    }

    public async Task<List<StockDto>> AddBatchAsync(List<StockInvokeDto> input)
    {
        var cargoIds = input.Select(a => a.CargoId).Distinct().ToList();
        var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));
        if (cargos.Count() != cargoIds.Count() || !cargoIds.All(a => cargos.Any(b => b.Id == a)))
            throw new BusinessException();

        List<HaoLife.Abp.Warehouse.Storehouses.Storelocation> storelocations = null;
        //判断是否开启仓库配置
        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            if (!await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StorehouseEnable))
                throw new BusinessException();
            var storelocationids = input.Where(a => a.StorelocationId.HasValue).Select(a => a.StorelocationId!.Value).Distinct().ToList();

            if (!storelocationids.Any())
                throw new BusinessException();

            storelocations = await this.LazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().GetListAsync(a => storelocationids.Contains(a.Id));
        }
        Expression<Func<Stock, bool>> func = (a) => false;

        foreach (var item in input)
        {
            func = func.Or(a => a.CargoId == item.CargoId
                && !a.IsFreeze
                && a.StorelocationId == item.StorelocationId
                && a.SeriesNumber == item.SeriesNumber
                && a.BatchNo == item.BatchNo);
        }

        var stocks = await this.stockRepository.GetListAsync(func);
        var addStocks = new List<Stock>();
        var logs = new List<StockOptLog>();
        foreach (var item in input)
        {
            var stock = stocks.FirstOrDefault(a => a.CargoId == item.CargoId
                && !a.IsFreeze
                && a.StorelocationId == item.StorelocationId
                && a.SeriesNumber == item.SeriesNumber
                && a.BatchNo == item.BatchNo);

            var cargo = cargos.First(a => a.Id == item.CargoId);
            var storelocation = storelocations?.FirstOrDefault(a => a.Id == item.StorelocationId);

            stock = await stockManager.AddAsync(stock, cargo, storelocation, item.Number, item.SeriesNumber, item.BatchNo, out bool isAdd);

            if (isAdd)
                addStocks.Add(stock);

            logs.Add(new StockOptLog(this.GuidGenerator.Create(), stock, item.Number, stock.Number - item.Number, stock.Number, StockOptType.In, this.CurrentTenant.Id));


        }

        if (stocks.Any())
            await this.stockRepository.UpdateManyAsync(stocks);
        if (addStocks.Any())
            await this.stockRepository.InsertManyAsync(addStocks);

        await this.stockOptLogRepository.InsertManyAsync(logs);

        var result = this.ObjectMapper.Map<List<Stock>, List<StockDto>>(stocks.Concat(addStocks).ToList());
        var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
        foreach (var item in result)
        {
            item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
        }

        return result;
    }

    public async Task<StockDto> DeductAsync(StockInvokeDto input)
    {
        var cargo = await this.cargoRepository.GetAsync(input.CargoId);

        HaoLife.Abp.Warehouse.Storehouses.Storelocation storelocation = null;
        //判断是否开启仓库配置
        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            if (!await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StorehouseEnable))
                throw new BusinessException();
            if (!input.StorelocationId.HasValue)
                throw new BusinessException();

            storelocation = await this.LazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().GetAsync(input.StorelocationId.Value);
        }

        var stock = await this.stockRepository.FirstOrDefaultAsync(a => a.CargoId == input.CargoId
            && !a.IsFreeze
            && a.StorelocationId == input.StorelocationId
            && a.SeriesNumber == input.SeriesNumber
            && a.BatchNo == input.BatchNo);

        if (stock == null) throw new BusinessException();

        stock.DeductNumber(input.Number);

        var log = new StockOptLog(this.GuidGenerator.Create(), stock, input.Number, stock.Number + input.Number, stock.Number, StockOptType.Out, this.CurrentTenant.Id);


        await this.stockRepository.UpdateAsync(stock);

        await this.stockOptLogRepository.InsertAsync(log);

        var result = this.ObjectMapper.Map<Stock, StockDto>(stock);

        result.Cargo = this.ObjectMapper.Map<Cargo, CargoListDto>(cargo);

        return result;
    }

    public async Task<List<StockDto>> DeductBatchAsync(List<StockInvokeDto> input)
    {
        var cargoIds = input.Select(a => a.CargoId).Distinct().ToList();
        var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));
        if (cargos.Count() != cargoIds.Count() || !cargoIds.All(a => cargos.Any(b => b.Id == a)))
            throw new BusinessException();

        List<HaoLife.Abp.Warehouse.Storehouses.Storelocation> storelocations = null;
        //判断是否开启仓库配置
        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            if (!await FeatureChecker.IsEnabledAsync(WarehouseFeatures.StorehouseEnable))
                throw new BusinessException();
            var storelocationids = input.Where(a => a.StorelocationId.HasValue).Select(a => a.StorelocationId!.Value).Distinct().ToList();

            if (!storelocationids.Any())
                throw new BusinessException();

            storelocations = await this.LazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().GetListAsync(a => storelocationids.Contains(a.Id));
        }
        Expression<Func<Stock, bool>> func = (a) => false;

        foreach (var item in input)
        {
            func = func.Or(a => a.CargoId == item.CargoId
                && !a.IsFreeze
                && a.StorelocationId == item.StorelocationId
                && a.SeriesNumber == item.SeriesNumber
                && a.BatchNo == item.BatchNo);
        }

        var stocks = await this.stockRepository.GetListAsync(func);
        var logs = new List<StockOptLog>();
        foreach (var item in input)
        {
            var stock = stocks.FirstOrDefault(a => a.CargoId == item.CargoId
                && !a.IsFreeze
                && a.StorelocationId == item.StorelocationId
                && a.SeriesNumber == item.SeriesNumber
                && a.BatchNo == item.BatchNo);

            if (stock == null) throw new BusinessException();


            var cargo = cargos.First(a => a.Id == item.CargoId);
            var storelocation = storelocations?.FirstOrDefault(a => a.Id == item.StorelocationId);

            stock.DeductNumber(item.Number);


            logs.Add(new StockOptLog(this.GuidGenerator.Create(), stock, item.Number, stock.Number + item.Number, stock.Number, StockOptType.Out, this.CurrentTenant.Id));


        }

        await this.stockRepository.UpdateManyAsync(stocks);
        await this.stockOptLogRepository.InsertManyAsync(logs);

        var result = this.ObjectMapper.Map<List<Stock>, List<StockDto>>(stocks);
        if (result != null && result.Count > 0)
        {

            var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
            foreach (var item in result)
            {
                item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
            }

        }
        return result!;
    }

    public async Task<StockDto> FreezeAsync(Guid id)
    {
        var stock = await this.stockRepository.GetAsync(id);
        stock.HandleFreeze(true);

        var log = new StockOptLog(this.GuidGenerator.Create(), stock, stock.Number, stock.Number, 0, StockOptType.Freeze, this.CurrentTenant.Id);

        await this.stockRepository.UpdateAsync(stock);

        await this.stockOptLogRepository.InsertAsync(log);


        var result = this.ObjectMapper.Map<Stock, StockDto>(stock);

        var cargo = await this.cargoRepository.GetAsync(stock.CargoId);
        result.Cargo = this.ObjectMapper.Map<Cargo, CargoListDto>(cargo);
        return result;
    }

    public async Task<List<StockDto>> FreezeBatchAsync(List<Guid> ids)
    {
        var stocks = await this.stockRepository.GetListAsync(a => ids.Contains(a.Id));
        var logs = new List<StockOptLog>();
        foreach (var stock in stocks)
        {
            stock.HandleFreeze(true);
            logs.Add(new StockOptLog(this.GuidGenerator.Create(), stock, stock.Number, stock.Number, 0, StockOptType.Freeze, this.CurrentTenant.Id));
        }

        await this.stockRepository.UpdateManyAsync(stocks);

        await this.stockOptLogRepository.InsertManyAsync(logs);


        var result = this.ObjectMapper.Map<List<Stock>, List<StockDto>>(stocks);

        if (result != null && result.Count > 0)
        {
            var cargoIds = stocks.Select(a => a.CargoId).Distinct().ToList();
            var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));
            var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
            foreach (var item in result)
            {
                item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
            }
        }
        return result!;
    }

    public async Task<StockDto> UnFreezeAsync(Guid id)
    {
        var stock = await this.stockRepository.GetAsync(id);
        stock.HandleFreeze(false);
        var log = new StockOptLog(this.GuidGenerator.Create(), stock, stock.Number, 0, stock.Number, StockOptType.UnFreeze, this.CurrentTenant.Id);

        await this.stockRepository.UpdateAsync(stock);

        await this.stockOptLogRepository.InsertAsync(log);


        var result = this.ObjectMapper.Map<Stock, StockDto>(stock);

        var cargo = await this.cargoRepository.GetAsync(stock.CargoId);
        result.Cargo = this.ObjectMapper.Map<Cargo, CargoListDto>(cargo);
        return result;
    }

    public async Task<List<StockDto>> UnFreezeBatchAsync(List<Guid> ids)
    {
        var stocks = await this.stockRepository.GetListAsync(a => ids.Contains(a.Id));
        var logs = new List<StockOptLog>();
        foreach (var stock in stocks)
        {
            stock.HandleFreeze(false);
            logs.Add(new StockOptLog(this.GuidGenerator.Create(), stock, stock.Number, 0, stock.Number, StockOptType.UnFreeze, this.CurrentTenant.Id));
        }

        await this.stockRepository.UpdateManyAsync(stocks);

        await this.stockOptLogRepository.InsertManyAsync(logs);

        var result = this.ObjectMapper.Map<List<Stock>, List<StockDto>>(stocks);
        if (result != null && result.Count > 0)
        {
            var cargoIds = stocks.Select(a => a.CargoId).Distinct().ToList();
            var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));
            var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
            foreach (var item in result)
            {
                item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
            }

        }
        return result!;
    }


    public Task<PagedResultDto<CargoStockDto>> GetCargoStockPagedListAsync(CargoStockSearchDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedResultDto<StockDto>> GetStackPagedListAsync(StockSearchDto input)
    {
        var query = await this.stockRepository.GetQueryableAsync();

        query = query.WhereIf(input.CargoId.HasValue, a => a.CargoId == input.CargoId);
        query = query.WhereIf(input.StorehouseId.HasValue, a => a.StorehouseId == input.StorehouseId);
        query = query.WhereIf(input.StoreareaId.HasValue, a => a.StoreareaId == input.StoreareaId);
        query = query.WhereIf(input.StorelocationId.HasValue, a => a.StorelocationId == input.StorelocationId);
        query = query.WhereIf(!input.CargoSn.IsNullOrWhiteSpace(), a => a.CargoSn == input.CargoSn);
        query = query.WhereIf(!input.SeriesNumber.IsNullOrWhiteSpace(), a => a.SeriesNumber == input.SeriesNumber);
        query = query.WhereIf(!input.BatchNo.IsNullOrWhiteSpace(), a => a.BatchNo == input.BatchNo);

        if (!input.CargoName.IsNullOrWhiteSpace()
            || !input.CargoBn.IsNullOrWhiteSpace())
        {
            var q1 = await this.cargoRepository.GetQueryableAsync();
            var q2 = query.GroupJoin(q1, a => a.CargoId, a => a.Id, (a, b) => new { a, b })
                 .SelectMany(a => a.b.DefaultIfEmpty(), (a, b) => new { a.a, b });

            q2 = q2.WhereIf(!input.CargoName.IsNullOrWhiteSpace(), a => a.b!.Name.Contains(input.CargoName!));
            q2 = q2.WhereIf(!input.CargoBn.IsNullOrWhiteSpace(), a => a.b!.Bn == input.CargoBn);

            query = q2.Select(a => a.a);

        }

        var count = query.Count();

        query = query.OrderByIf<Stock, IQueryable<Stock>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);

        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();

        var result = this.ObjectMapper.Map<List<Stock>, List<StockDto>>(ls);

        if (result != null && result.Count > 0)
        {
            var cargoIds = ls.Select(a => a.CargoId).Distinct().ToList();
            var cargos = await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id));

            var cargoMap = this.ObjectMapper.Map<List<Cargo>, List<CargoListDto>>(cargos).ToDictionary(a => a.Id);
            foreach (var item in result)
            {
                item.Cargo = cargoMap.GetOrDefault(item.CargoId)!;
            }

        }

        return new PagedResultDto<StockDto>(count, result!);
    }

}
