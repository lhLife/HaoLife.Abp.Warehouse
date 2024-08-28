using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStoreareaRepository : IRepository<Storearea, Guid>
{
    Task<List<Storearea>> GetListAsync(List<Guid> ids);
}
