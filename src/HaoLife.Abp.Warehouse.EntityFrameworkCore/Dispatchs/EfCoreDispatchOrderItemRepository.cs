using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Dispatchs;

public class EfCoreDispatchOrderItemRepository : EfCoreRepository<WarehouseDbContext, DispatchOrderItem, Guid>
    , IDispatchOrderItemRepository
{
    public EfCoreDispatchOrderItemRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}
