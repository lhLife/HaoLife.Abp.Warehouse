using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;

[GlobalFeatureName(Name)]
public class CargoCategoryFeature : GlobalFeature
{
    public const string Name = "Warehouse.CargoCategory";

    internal CargoCategoryFeature(
        GlobalWarehouseFeatures warehouse
    ) : base(warehouse)
    {

    }

}
