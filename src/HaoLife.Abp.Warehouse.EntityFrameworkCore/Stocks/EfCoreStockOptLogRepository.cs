using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Stocks;

public class EfCoreStockOptLogRepository : EfCoreRepository<WarehouseDbContext, StockOptLog, Guid>
    , IStockOptLogRepository
{
    public EfCoreStockOptLogRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
