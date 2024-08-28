using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Storehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace HaoLife.Abp.Warehouse.Stocks;

public class StockManager : DomainService
{

    public Task<Stock> AddAsync(Stock? stock, Cargo cargo, Storelocation? storelocation, int number, string? seriesNumber, string? batchNo, out bool isAdd)
    {
        isAdd = false;
        if (stock == null)
        {
            stock = new Stock(this.GuidGenerator.Create(), cargo.Id, cargo.Sn, this.CurrentTenant.Id);
            stock.SetSeriesNumber(seriesNumber);
            stock.SetBatchNo(batchNo);
            stock.SetStorelocation(storelocation?.StorehouseId, storelocation?.StoreareaId, storelocation?.Id);
            isAdd = true;
        }
        stock.AddNumber(number);

        return Task.FromResult(stock);
    }



}
