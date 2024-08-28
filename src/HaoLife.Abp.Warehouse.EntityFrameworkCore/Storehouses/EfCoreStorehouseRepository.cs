using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class EfCoreStorehouseRepository : EfCoreRepository<WarehouseDbContext, Storehouse, Guid>
    , IStorehouseRepository
{
    public EfCoreStorehouseRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {

    }

    public Task<List<Storehouse>> GetListAsync(List<Guid> ids)
    {
        return this.GetListAsync(a => ids.Contains(a.Id), false);
    }
}
