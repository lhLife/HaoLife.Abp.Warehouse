using HaoLife.Abp.Warehouse.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace HaoLife.Abp.Warehouse.Suppliers;

public class EfCoreSupplierRepository : EfCoreRepository<WarehouseDbContext, Supplier, Guid>
    , ISupplierRepository
{
    public EfCoreSupplierRepository(IDbContextProvider<WarehouseDbContext> dbContextProvider) 
        : base(dbContextProvider)
    {
    }
}
