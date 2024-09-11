using HaoLife.Abp.Warehouse.Cargos;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Xunit;

namespace HaoLife.Abp.Warehouse.Stocks;

public class StockManager_Tests : WarehouseDomainTestBase<WarehouseDomainTestModule>
{
    private readonly StockManager _stockManager;
    public StockManager_Tests()
    {
        _stockManager = GetRequiredService<StockManager>();
    }


    [Fact]
    public async Task StockNullAddAsync()
    {
        var tenantId = this.CurrentTenant.Id;
        var number = 100;
        var seriesNumber = "sn_123";
        var batchNo = "bn_123";
        var cargo = new Cargo(new Guid("10000000-0000-0000-0000-000000000001"), "货物1", "images", "b123", "s123", true, 100, "g", "颜色:红", 99, tenantId);
        var storelocation = new global::HaoLife.Abp.Warehouse.Storehouses.Storelocation(new Guid("00000000-0000-0000-0000-000000000001"), "L123", true, new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), tenantId);
        var stock = await this._stockManager.AddAsync(null, cargo, storelocation, number, seriesNumber, batchNo, out bool add);

        add.ShouldBeTrue();
        stock.TenantId.ShouldBe(tenantId);
        stock.CargoId.ShouldBe(cargo.Id);
        stock.CargoSn.ShouldBe(cargo.Sn);
        stock.Number.ShouldBe(number);
        stock.SeriesNumber.ShouldBe(seriesNumber);
        stock.BatchNo.ShouldBe(batchNo);
        stock.StorehouseId.ShouldBe(storelocation.StorehouseId);
        stock.StoreareaId.ShouldBe(storelocation.StoreareaId);
        stock.StorelocationId.ShouldBe(storelocation.Id);

    }

    [Fact]
    public async Task StockNotNullAddAsync()
    {
        var tenantId = this.CurrentTenant.Id;
        var number = 100;
        var seriesNumber = "sn_123";
        var batchNo = "bn_123";
        var cargo = new Cargo(new Guid("10000000-0000-0000-0000-000000000001"), "货物1", "images", "b123", "s123", true, 100, "g", "颜色:红", 99, tenantId);
        var storelocation = new global::HaoLife.Abp.Warehouse.Storehouses.Storelocation(new Guid("00000000-0000-0000-0000-000000000001"), "L123", true, new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000001"), tenantId);
        var stock1 = new Stock(Guid.Empty, cargo.Id, cargo.Sn, tenantId);
        stock1.SetSeriesNumber(seriesNumber);
        stock1.SetBatchNo(batchNo);
        stock1.SetStorelocation(storelocation.StorehouseId, storelocation.StoreareaId, storelocation.Id);

        var stock = await this._stockManager.AddAsync(stock1, cargo, storelocation, number, seriesNumber, batchNo, out bool add);

        add.ShouldBeFalse();
        stock.TenantId.ShouldBe(tenantId);
        stock.CargoId.ShouldBe(cargo.Id);
        stock.CargoSn.ShouldBe(cargo.Sn);
        stock.Number.ShouldBe(number);
        stock.SeriesNumber.ShouldBe(seriesNumber);
        stock.BatchNo.ShouldBe(batchNo);
        stock.StorehouseId.ShouldBe(storelocation.StorehouseId);
        stock.StoreareaId.ShouldBe(storelocation.StoreareaId);
        stock.StorelocationId.ShouldBe(storelocation.Id);

    }
}
