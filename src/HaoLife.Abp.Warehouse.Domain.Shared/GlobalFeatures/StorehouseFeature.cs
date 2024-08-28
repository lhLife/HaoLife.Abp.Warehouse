using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;


[GlobalFeatureName(Name)]
public class StorehouseFeature : GlobalFeature
{
    public const string Name = "Warehouse.Storehouse";

    internal StorehouseFeature(
        GlobalWarehouseFeatures warehouse
    ) : base(warehouse)
    {

    }

}
