using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using HaoLife.Abp.Warehouse.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class EfCoreStoreToolRepository : EfCoreRepository<WarehouseDbContext, StoreTool, Guid>
    , IStoreToolRepository
{
    public EfCoreStoreToolRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }
}
