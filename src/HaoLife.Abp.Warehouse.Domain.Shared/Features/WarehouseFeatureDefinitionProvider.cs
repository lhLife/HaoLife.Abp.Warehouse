using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;

namespace HaoLife.Abp.Warehouse.Features;

public class WarehouseFeatureDefinitionProvider : FeatureDefinitionProvider
{
    public override void Define(IFeatureDefinitionContext context)
    {

        var group = context.AddGroup(WarehouseFeatures.GroupName,
            L("Feature:WarehouseGroup"));

        group.AddFeature(WarehouseFeatures.CargoEnable,
        "true",
        L("Feature:CargoEnable"),
        L("Feature:CargoEnableDescription"),
        new ToggleStringValueType());

        if (GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>())
        {
            group.AddFeature(WarehouseFeatures.CargoCategoryEnable,
            "true",
            L("Feature:CargoCategoryEnable"),
            L("Feature:CargoCategoryEnableDescription"),
            new ToggleStringValueType());
        }

        if (GlobalFeatureManager.Instance.IsEnabled<CargoTypeSpecFeature>())
        {
            group.AddFeature(WarehouseFeatures.CargoTypeSpecEnable,
            "true",
            L("Feature:CargoTypeSpecEnable"),
            L("Feature:CargoTypeSpecEnableDescription"),
            new ToggleStringValueType());
        }

        if (GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>())
        {
            group.AddFeature(WarehouseFeatures.SupplierEnable,
            "true",
            L("Feature:SupplierEnable"),
            L("Feature:SupplierEnableDescription"),
            new ToggleStringValueType());
        }


        if (GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>())
        {
            group.AddFeature(WarehouseFeatures.StoreToolEnable,
            "true",
            L("Feature:StoreToolEnable"),
            L("Feature:StoreToolEnableDescription"),
            new ToggleStringValueType());
        }


        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            group.AddFeature(WarehouseFeatures.StorehouseEnable,
            "true",
            L("Feature:StorehouseEnable"),
            L("Feature:StorehouseEnableDescription"),
            new ToggleStringValueType());
        }
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WarehouseResource>(name);
    }
}
