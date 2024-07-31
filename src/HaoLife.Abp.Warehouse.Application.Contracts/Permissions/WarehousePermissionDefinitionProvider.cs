using HaoLife.Abp.Warehouse.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace HaoLife.Abp.Warehouse.Permissions;

public class WarehousePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WarehousePermissions.GroupName, L("Permission:Warehouse"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<WarehouseResource>(name);
    }
}
