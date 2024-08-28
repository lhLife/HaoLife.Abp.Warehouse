using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Stocks;

public class EfCoreStockRepository : EfCoreRepository<WarehouseDbContext, Stock, Guid>
    , IStockRepository
{
    public EfCoreStockRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
