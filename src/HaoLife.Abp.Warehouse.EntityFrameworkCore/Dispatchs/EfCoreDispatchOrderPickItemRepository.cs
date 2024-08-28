using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Dispatchs;

public class EfCoreDispatchOrderPickItemRepository : EfCoreRepository<WarehouseDbContext, DispatchOrderPickItem, Guid>
    , IDispatchOrderPickItemRepository
{
    public EfCoreDispatchOrderPickItemRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}
