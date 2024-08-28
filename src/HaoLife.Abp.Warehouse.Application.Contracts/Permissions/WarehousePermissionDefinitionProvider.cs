using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;

namespace HaoLife.Abp.Warehouse.Permissions;

public class WarehousePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var wmsGroup = context.GetGroupOrNull(WarehousePermissions.GroupName) ?? context.AddGroup(WarehousePermissions.GroupName, L("Permission:Warehouse"));


        var cargoCategory = wmsGroup.AddPermission(WarehousePermissions.CargoCategory.Default, L("Permission:CargoCategory"))
            .RequireGlobalFeatures(typeof(CargoCategoryFeature));
        cargoCategory.AddChild(WarehousePermissions.CargoCategory.Create, L("Permission:CargoCategory.Create"))
            .RequireGlobalFeatures(typeof(CargoCategoryFeature));
        cargoCategory.AddChild(WarehousePermissions.CargoCategory.Update, L("Permission:CargoCategory.Update"))
            .RequireGlobalFeatures(typeof(CargoCategoryFeature));
        cargoCategory.AddChild(WarehousePermissions.CargoCategory.Delete, L("Permission:CargoCategory.Delete"))
            .RequireGlobalFeatures(typeof(CargoCategoryFeature));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WarehouseResource>(name);
    }
}
