using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;

[GlobalFeatureName(Name)]
public class SupplierFeature : GlobalFeature
{
    public const string Name = "Warehouse.Supplier";
    internal SupplierFeature(
        GlobalWarehouseFeatures warehouse)
        : base(warehouse)
    {
    }
}
