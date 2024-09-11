using HaoLife.Abp.Warehouse.Arriveds;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Stocks;
using HaoLife.Abp.Warehouse.Storehouses;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Xunit.Abstractions;

namespace HaoLife.Abp.Warehouse;

public class WarehouseDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly ICurrentTenant _currentTenant;
    private readonly ICargoRepository cargoRepository;
    private readonly IArrivedOrderRepository arrivedOrderRepository;
    private readonly IStorehouseRepository storehouseRepository;
    private readonly IStoreareaRepository storeareaRepository;
    private readonly IStorelocationRepository storelocationRepository;

    public WarehouseDataSeedContributor(
        IGuidGenerator guidGenerator, ICurrentTenant currentTenant
        , ICargoRepository cargoRepository
        , IArrivedOrderRepository arrivedOrderRepository
        , IStorehouseRepository storehouseRepository
        , IStoreareaRepository storeareaRepository
        , IStorelocationRepository storelocationRepository)
    {
        _guidGenerator = guidGenerator;
        _currentTenant = currentTenant;
        this.cargoRepository = cargoRepository;
        this.arrivedOrderRepository = arrivedOrderRepository;
        this.storehouseRepository = storehouseRepository;
        this.storeareaRepository = storeareaRepository;
        this.storelocationRepository = storelocationRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        /* Instead of returning the Task.CompletedTask, you can insert your test data
         * at this point!
         */

        using (_currentTenant.Change(context?.TenantId))
        {
            await AddDateAsync();
        }
    }


    public async Task AddDateAsync()
    {
        var cargos = new[]
        {
            new Cargo(new Guid("10000000-0000-0000-0000-000000000001"),"货物1","http://www.baidu.com/i1","bn1231","sn1231",true,100,"g","颜色:红",100),
            new Cargo(new Guid("10000000-0000-0000-0000-000000000002"),"货物2","http://www.baidu.com/i1","bn1232","sn1232",true,100,"g","颜色:黄",100),
            new Cargo(new Guid("10000000-0000-0000-0000-000000000003"),"货物3","http://www.baidu.com/i1","bn1233","sn1233",true,100,"g","颜色:蓝",100),
            new Cargo(new Guid("10000000-0000-0000-0000-000000000004"),"货物4","http://www.baidu.com/i1","bn1234","sn1234",true,100,"g","颜色:绿",100),
        };
        await this.cargoRepository.InsertManyAsync(cargos);

        var orders = new[] {
            new ArrivedOrder(new Guid("20000000-0000-0000-0000-000000000001"),"no1","bt1",new DateOnly(2024,9,11),"我","159xx"),
            new ArrivedOrder(new Guid("20000000-0000-0000-0000-000000000002"),"no2","bt2",new DateOnly(2024,9,11),"我","159xx"),
            new ArrivedOrder(new Guid("20000000-0000-0000-0000-000000000003"),"no3","bt3",new DateOnly(2024,9,11),"我","159xx"),
            new ArrivedOrder(new Guid("20000000-0000-0000-0000-000000000004"),"no4","bt4",new DateOnly(2024,9,11),"我","159xx"),
            new ArrivedOrder(new Guid("20000000-0000-0000-0000-000000000005"),"no4","bt4",new DateOnly(2024,9,11),"我","159xx"),
        };

        foreach (var order in orders)
        {
            var cargo = cargos[0];
            order.AddItem(Guid.NewGuid(), cargo.Id, cargo.Name, cargo.Bn, cargo.Sn, cargo.SpecDesc, 10, 100);
        }

        foreach (var order in orders.Skip(1))
        {
            order.Arrived(new DateTime(2024, 9, 11));
        }

        foreach (var order in orders.Skip(2))
        {
            order.Unload(new DateTime(2024, 9, 11), "hao");
        }
        foreach (var order in orders.Skip(3))
        {
            var i = order.Id.ToString().LastOrDefault();
            var cargo = cargos[0];
            var item2 = order.Items.FirstOrDefault(a => a.CargoId == cargo.Id);
            order.AddPick(item2!, 3, new System.Collections.Generic.List<string> { $"{i}_sn1", $"{i}_sn2", $"{i}_sn3", }, () => Guid.NewGuid());
            order.Pick();
        }

        foreach (var order in orders.Skip(4))
        {
            order.Stock();
        }
        await this.arrivedOrderRepository.InsertManyAsync(orders);


        var storehouses = new[] {
            new Storehouse(new Guid("11000000-0000-0000-0000-000000000001"),"仓库1","350001","福州","鬼知道","jk@qq.com","150x","hao",true)
        };
        var storearea = new[] {
            new Storearea(new Guid("11100000-0000-0000-0000-000000000001"),"库区1", StoreareaType.None,true,storehouses[0].Id),
        };

        var storelocation = new[] {
            new Storelocation(new Guid("11110000-0000-0000-0000-000000000001"),"l1",true,storearea[0].StorehouseId,storearea[0].Id),
            new Storelocation(new Guid("11110000-0000-0000-0000-000000000002"),"l2",true,storearea[0].StorehouseId,storearea[0].Id)
        };

        await this.storehouseRepository.InsertManyAsync(storehouses);
        await this.storeareaRepository.InsertManyAsync(storearea);
        await this.storelocationRepository.InsertManyAsync(storelocation);
    }
}
