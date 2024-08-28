using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.GlobalFeatures;


[GlobalFeatureName(Name)]
public class StockProcessFeature : GlobalFeature
{
    public const string Name = "Warehouse.StockProcess";

    internal StockProcessFeature(
        GlobalWarehouseFeatures warehouse
    ) : base(warehouse)
    {

    }

}
