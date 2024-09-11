using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace HaoLife.Abp.Warehouse.Arriveds;

public abstract class ArrivedOrderAppService_Tests<TStartupModule> : WarehouseApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IArrivedOrderAppService _arrivedOrderAppService;
    public ArrivedOrderAppService_Tests()
    {
        _arrivedOrderAppService = GetRequiredService<IArrivedOrderAppService>();
    }



    [Fact]
    public async Task CreateAsync()
    {
        var dto = new ArrivedOrderCreateDto()
        {
            BatchNo = "b123",
            ExpectArrivedDate = DateOnly.Parse("2021/10/1"),

            Items = new List<ArrivedOrderItemCreateDto>()
            {
                new ArrivedOrderItemCreateDto(){
                    CargoId=new Guid("10000000-0000-0000-0000-000000000001"),
                    CostPrice=101,
                    Number=10,
                }
            }
        };

        var model = await this._arrivedOrderAppService.CreateAsync(dto);

        model.OrderNo.ShouldStartWith($"A{DateTime.Now.ToString("yyMMddHHmmss")}");
        model.ExpectArrivedDate.ShouldBe(dto.ExpectArrivedDate);
        model.BatchNo.ShouldBe(dto.BatchNo);
        model.Status.ShouldBe(ArrivedStatus.PreArrived);
        model.Items.Count.ShouldBe(1);
        model.Items.FirstOrDefault()!.Sn.ShouldBe("sn1231");
        model.Items.FirstOrDefault()!.Bn.ShouldBe("bn1231");
        model.Items.FirstOrDefault()!.SpecDesc.ShouldBe("颜色:红");

    }

    [Fact]
    public async Task ArrivedAsync()
    {
        var dto = new ArrivedOrderArrivedDto()
        {
            ArrivedDate = DateTime.Now,
        };

        var model = await this._arrivedOrderAppService.ArrivedAsync(new Guid("20000000-0000-0000-0000-000000000001"), dto);

        model.Status.ShouldBe(ArrivedStatus.ToBeUnload);
        model.ArrivedDate.ShouldBe(dto.ArrivedDate);
        model.Items.Count.ShouldBe(1);

    }


    [Fact]
    public async Task UnloadAsync()
    {
        var dto = new ArrivedOrderUnloadDto()
        {
            UnloadTime = DateTime.Now,
            UnloadOperator = "hao",
        };

        var model = await this._arrivedOrderAppService.UnloadAsync(new Guid("20000000-0000-0000-0000-000000000002"), dto);

        model.Status.ShouldBe(ArrivedStatus.UnPick);
        model.UnloadTime.ShouldBe(dto.UnloadTime);
        model.UnloadOperator.ShouldBe(dto.UnloadOperator);
        model.Items.Count.ShouldBe(1);

    }


    [Fact]
    public async Task AddPickItemCountEqSnAsync()
    {
        var dto = new List<ArrivedOrderPickDto>()
        {
            new ArrivedOrderPickDto(){
                CargoId=new Guid("10000000-0000-0000-0000-000000000001"),
                Number=3,
                SeriesNumbers=new List<string>(){ "a1","a2","a3"}
            }
        };

        var model = await this._arrivedOrderAppService.AddPickItemAsync(new Guid("20000000-0000-0000-0000-000000000003"), dto);

        model.Status.ShouldBe(ArrivedStatus.UnPick);
        model.Items.Count.ShouldBe(1);
        model.Items.FirstOrDefault()!.Picks.Count.ShouldBe(3);
        model.Items.FirstOrDefault()!.Picks.ElementAt(0)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(0));
        model.Items.FirstOrDefault()!.Picks.ElementAt(1)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(1));
        model.Items.FirstOrDefault()!.Picks.ElementAt(2)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(2));

    }


    [Fact]
    public async Task AddPickItemCountNeqSnAsync()
    {
        var dto = new List<ArrivedOrderPickDto>()
        {
            new ArrivedOrderPickDto(){
                CargoId=new Guid("10000000-0000-0000-0000-000000000001"),
                Number=5,
                SeriesNumbers=new List<string>(){ "a1","a2","a3"}
            }
        };

        var model = await this._arrivedOrderAppService.AddPickItemAsync(new Guid("20000000-0000-0000-0000-000000000003"), dto);

        model.Status.ShouldBe(ArrivedStatus.UnPick);
        model.Items.Count.ShouldBe(1);
        model.Items.FirstOrDefault()!.Picks.Count.ShouldBe(4);
        model.Items.FirstOrDefault()!.Picks.ElementAt(0)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(0));
        model.Items.FirstOrDefault()!.Picks.ElementAt(1)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(1));
        model.Items.FirstOrDefault()!.Picks.ElementAt(2)!.SeriesNumber.ShouldBe(dto[0].SeriesNumbers!.ElementAt(2));
        model.Items.FirstOrDefault()!.Picks.ElementAt(3)!.SeriesNumber.ShouldBeNullOrEmpty();

    }



    [Fact]
    public async Task PickAsync()
    {
        var model = await this._arrivedOrderAppService.PickAsync(new Guid("20000000-0000-0000-0000-000000000003"));

        model.Status.ShouldBe(ArrivedStatus.UnPutAway);
        model.Items.Count.ShouldBe(1);
    }

    [Fact]
    public async Task StockAsync()
    {
        var model1 = await this._arrivedOrderAppService.GetAsync(new Guid("20000000-0000-0000-0000-000000000004"));

        var dto = model1.Items
             .SelectMany(a => a.Picks, (a, b) => new ArrivedOrderStockDto()
             {
                 ItemId = a.Id,
                 PickItemId = b.Id,
                 StorelocationId = new Guid("11110000-0000-0000-0000-000000000001")
             }).ToList();

        var model = await this._arrivedOrderAppService.StockAsync(new Guid("20000000-0000-0000-0000-000000000004"), dto);

        model.Status.ShouldBe(ArrivedStatus.Completed);
        model.Items.Count.ShouldBe(1);

    }
}
