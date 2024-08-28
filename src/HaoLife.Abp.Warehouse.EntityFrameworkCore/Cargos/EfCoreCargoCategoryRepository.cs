using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Cargos;

public class EfCoreCargoCategoryRepository : EfCoreRepository<WarehouseDbContext, CargoCategory, Guid>
    , ICargoCategoryRepository
{
    public EfCoreCargoCategoryRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

    public virtual async Task<Dictionary<Guid, int>> GetChildCountAsync(List<Guid> ids)
    {
        var query = await this.GetQueryableAsync();

        var counts = query.Where(a => ids.Contains(a.ParentId.Value)).GroupBy(a => a.ParentId.Value, (a, b) => new { id = a, count = b.Count() }).ToList();

        return counts.ToDictionary(a => a.id, a => a.count);
    }

    public virtual Task<List<CargoCategory>> GetChildsAsync(Guid parentId)
    {
        return this.GetListAsync(a => a.ParentId == parentId);
    }

    public virtual Task<List<CargoCategory>> GetRootsAsync()
    {
        return this.GetListAsync(a => a.ParentId == null);
    }
}
