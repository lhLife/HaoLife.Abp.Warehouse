using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;


[GlobalFeatureName(Name)]
public class CargoTypeSpecFeature : GlobalFeature
{
    public const string Name = "Warehouse.CargoTypeSpec";

    internal CargoTypeSpecFeature(
        GlobalWarehouseFeatures warehouse
    ) : base(warehouse)
    {

    }

}
