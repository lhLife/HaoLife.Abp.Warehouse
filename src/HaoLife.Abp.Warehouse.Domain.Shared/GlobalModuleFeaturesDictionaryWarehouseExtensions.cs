using HaoLife.Abp.Warehouse.GlobalFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volo.Abp.GlobalFeatures;

public static class GlobalModuleFeaturesDictionaryWarehouseExtensions
{
    public static GlobalWarehouseFeatures Warehouse(
        this GlobalModuleFeaturesDictionary modules)
    {
        Check.NotNull(modules, nameof(modules));

        return modules
                .GetOrAdd(
                    GlobalWarehouseFeatures.ModuleName,
                    _ => new GlobalWarehouseFeatures(modules.FeatureManager)
                )
            as GlobalWarehouseFeatures;
    }

    public static GlobalModuleFeaturesDictionary Warehouse(
        this GlobalModuleFeaturesDictionary modules,
        Action<GlobalWarehouseFeatures> configureAction)
    {
        Check.NotNull(configureAction, nameof(configureAction));

        configureAction(modules.Warehouse());

        return modules;
    }
}
