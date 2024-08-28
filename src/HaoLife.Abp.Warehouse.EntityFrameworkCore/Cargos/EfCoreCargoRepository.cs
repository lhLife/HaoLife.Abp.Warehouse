using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Cargos;

public class EfCoreCargoRepository : EfCoreRepository<WarehouseDbContext, Cargo, Guid>
    , ICargoRepository
{
    public EfCoreCargoRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider)
        : base(dbContextProvider)
    {
    }

}
