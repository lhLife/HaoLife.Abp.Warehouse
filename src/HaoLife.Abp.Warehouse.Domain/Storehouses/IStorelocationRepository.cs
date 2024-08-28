using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace HaoLife.Abp.Warehouse.Storehouses;

public interface IStorelocationRepository : IRepository<Storelocation, Guid>
{
}
