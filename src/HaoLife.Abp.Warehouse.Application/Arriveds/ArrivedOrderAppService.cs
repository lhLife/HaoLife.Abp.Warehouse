using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Features;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Permissions;
using HaoLife.Abp.Warehouse.Settings;
using HaoLife.Abp.Warehouse.Storehouses;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Settings;

namespace HaoLife.Abp.Warehouse.Arriveds;

//[RequiresFeature(WarehouseFeatures.ArrivedOrderEnable)]
//[Authorize(WarehousePermissions.ArrivedOrder.Default)]
//[RequiresGlobalFeature(typeof(ArrivedOrderFreature))]
public class ArrivedOrderAppService : WarehouseAppService, IArrivedOrderAppService
{
    private readonly IArrivedOrderRepository arrivedOrderRepository;
    private readonly ICargoRepository cargoRepository;
    private readonly IStorelocationRepository storelocationRepository;

    public ArrivedOrderAppService(IArrivedOrderRepository arrivedOrderRepository
        , ICargoRepository cargoRepository
        , IStorelocationRepository storelocationRepository)
    {
        this.arrivedOrderRepository = arrivedOrderRepository;
        this.cargoRepository = cargoRepository;
        this.storelocationRepository = storelocationRepository;
    }

    public async Task<ArrivedOrderDto> ArrivedAsync(Guid id, ArrivedOrderArrivedDto input)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);
        if (!model.IsArrived()) throw new BusinessException();

        model.Arrived(input.ArrivedDate);

        await this.arrivedOrderRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task<ArrivedOrderDto> CreateAsync(ArrivedOrderCreateDto input)
    {
        var orderNoTemplate = await this.SettingProvider.GetOrNullAsync(WarehouseSettings.ArrivedOrderNoGenerateTemplate)
            ?? CommonConsts.ArrivedOrderNoDefaultTemplate;

        var orderNo = await Scriban.Template.Parse(orderNoTemplate).RenderAsync(new
        {
            d = DateTime.Now,
            r = new Random().Next(100000000, 999999999),
            input.BatchNo,
        });

        var cargoIds = input.Items.Select(a => a.CargoId).Distinct().ToList();
        var cargoMap = (await this.cargoRepository.GetListAsync(a => cargoIds.Contains(a.Id))).ToDictionary(a => a.Id);

        if (cargoMap.Keys.Count != cargoIds.Count || !input.Items.All(a => cargoMap.ContainsKey(a.CargoId)))
            throw new BusinessException();

        var model = new ArrivedOrder(this.GuidGenerator.Create(), orderNo, input.BatchNo, input.ExpectArrivedDate
            , input.Contacts, input.ContactsPhone, input.Memo, this.CurrentTenant.Id);

        input.Items.ForEach(a =>
        {
            var cargo = cargoMap[a.CargoId];
            model.AddItem(this.GuidGenerator.Create(), a.CargoId, cargo.Name, cargo.Bn, cargo.Sn, cargo.SpecDesc, a.Number, a.CostPrice);
        });

        await this.arrivedOrderRepository.InsertAsync(model);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);
        if (model.IsRemove()) throw new BusinessException();

        await this.arrivedOrderRepository.DeleteAsync(model);
    }

    public async Task<ArrivedOrderDto> GetAsync(Guid id)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task<PagedResultDto<ArrivedOrderListDto>> GetListAsync(ArrivedOrderSearchDto input)
    {
        var query = await this.arrivedOrderRepository.GetQueryableAsync();

        query = query.WhereIf(!input.OrderNo.IsNullOrWhiteSpace(), a => a.OrderNo == input.OrderNo);
        query = query.WhereIf(!input.BatchNo.IsNullOrWhiteSpace(), a => a.BatchNo == input.BatchNo);
        query = query.WhereIf(!input.Contacts.IsNullOrWhiteSpace(), a => a.Contacts!.Contains(input.Contacts!));
        query = query.WhereIf(!input.ContactsPhone.IsNullOrWhiteSpace(), a => a.ContactsPhone!.Contains(input.ContactsPhone!));
        query = query.WhereIf(!input.Memo.IsNullOrWhiteSpace(), a => a.Memo!.Contains(input.Memo!));
        query = query.WhereIf(input.BeginExpectArrivedDate.HasValue, a => a.ExpectArrivedDate >= input.BeginExpectArrivedDate);
        query = query.WhereIf(input.EndExpectArrivedDate.HasValue, a => a.ExpectArrivedDate <= input.EndExpectArrivedDate);

        var count = query.Count();

        query = query.OrderByIf<ArrivedOrder, IQueryable<ArrivedOrder>>(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting!);


        //limit 要放到最后，否则 语法会复杂化
        query = query.PageBy(input.SkipCount, input.MaxResultCount);
        var ls = query.ToList();


        var result = this.ObjectMapper.Map<List<ArrivedOrder>, List<ArrivedOrderListDto>>(ls);

        return new PagedResultDto<ArrivedOrderListDto>(count, result);
    }

    public async Task<ArrivedOrderDto> AddPickItemAsync(Guid id, List<ArrivedOrderPickDto> input)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);

        var ids = input.Select(a => a.CargoId).ToList();

        if (!model.IsPickItem(ids)) throw new BusinessException();

        if (!input.All(a => model.CheckPickSeriesNumbers(a.Number, a.SeriesNumbers))) throw new BusinessException();

        foreach (var item in input)
        {
            var item2 = model.Items.First(a => a.CargoId == item.CargoId);

            model.AddPick(item2, item.Number, item.SeriesNumbers, () => this.GuidGenerator.Create());
        }

        await this.arrivedOrderRepository.UpdateAsync(model);


        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task<ArrivedOrderDto> UnloadAsync(Guid id, ArrivedOrderUnloadDto input)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);
        if (!model.IsUnload()) throw new BusinessException();

        model.Unload(input.UnloadTime, input.UnloadOperator!);

        await this.arrivedOrderRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task<ArrivedOrderDto> PickAsync(Guid id)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);
        if (!model.IsPick()) throw new BusinessException();

        model.Pick();

        await this.arrivedOrderRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }

    public async Task<ArrivedOrderDto> StockAsync(Guid id, List<ArrivedOrderStockDto> stocks)
    {
        var model = await this.arrivedOrderRepository.GetAsync(id);
        if (!model.IsStock()) throw new BusinessException();

        var ids = stocks.Select(a => a.StorelocationId).Distinct().ToList();
        var storelocations = await this.storelocationRepository.GetListAsync(a => ids.Contains(a.Id));
        var storelocationMap = storelocations.ToDictionary(a => a.Id);

        var pickitems = model.Items.SelectMany(a => a.Picks, (a, b) => new Tuple<ArrivedOrderItem, ArrivedOrderPickItem>(a, b));
        foreach (var item in stocks)
        {
            var pick = pickitems.FirstOrDefault(a => a.Item1.Id == item.ItemId && a.Item2.Id == item.PickItemId);
            if (pick == null || pick.Item2 == null || pick.Item1 == null) throw new BusinessException();

            model.SetStorelocation(pick.Item1, pick.Item2!, item.StorelocationId, storelocationMap[item.StorelocationId].Code);
        }

        if (!model.IsAllItemStock()) throw new BusinessException();

        model.Stock();

        await this.arrivedOrderRepository.UpdateAsync(model);

        return this.ObjectMapper.Map<ArrivedOrder, ArrivedOrderDto>(model);
    }
}
