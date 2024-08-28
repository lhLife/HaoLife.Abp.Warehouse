using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStorehouseRepository : IRepository<Storehouse, Guid>
{
    Task<List<Storehouse>> GetListAsync(List<Guid> ids);
}
