using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HaoLife.Abp.Warehouse.Stocks;

public class Stock_Tests
{
    [Fact]
    public void Ctor()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.CargoId.ShouldBe(cargoid);
        stock.CargoSn.ShouldBe(sn);
    }

    [Fact]
    public void SetCargoId()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.SetCargoId(cargoid);
        stock.CargoId.ShouldBe(cargoid);
    }

    [Fact]
    public void SetCargoSn()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.SetCargoSn(sn);
        stock.CargoSn.ShouldBe(sn);
    }

    [Fact]
    public void AddNumber()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.AddNumber(10);
        stock.Number.ShouldBe(10);
    }

    [Fact]
    public void DeductNumber()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.AddNumber(10);
        stock.DeductNumber(5);
        stock.Number.ShouldBe(5);
    }

    [Fact]
    public void SetStorelocation()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var sid = Guid.NewGuid();
        var sid2 = Guid.NewGuid();
        var sid3 = Guid.NewGuid();
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.SetStorelocation(sid, sid2, sid3);
        stock.StorehouseId.ShouldBe(sid);
        stock.StoreareaId.ShouldBe(sid2);
        stock.StorelocationId.ShouldBe(sid3);
    }
    [Fact]
    public void SetSeriesNumber()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var ssn = "a123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.SetSeriesNumber(ssn);
        stock.SeriesNumber.ShouldBe(ssn);
    }
    [Fact]
    public void SetBatchNo()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var batchno = "b123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.SetBatchNo(batchno);
        stock.BatchNo.ShouldBe(batchno);
    }

    [Fact]
    public void HandleFreeze()
    {
        var cargoid = Guid.NewGuid();
        var sn = "s123";
        var stock = new Stock(Guid.NewGuid(), cargoid, sn);
        stock.HandleFreeze(true);
        stock.IsFreeze.ShouldBeTrue();
    }
}
