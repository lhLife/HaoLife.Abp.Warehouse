using Volo.Abp.Reflection;

namespace HaoLife.Abp.Warehouse.Permissions;

public class WarehousePermissions
{
    public const string GroupName = "Warehouse";

    public static class Cargo
    {
        public const string Default = GroupName + ".Cargo";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class CargoCategory
    {
        public const string Default = GroupName + ".CargoCategory";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class CargoTypeSpec
    {
        public const string Default = GroupName + ".CargoTypeSpec";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Storehouse
    {
        public const string Default = GroupName + ".Storehouse";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static class Storelocation
    {
        public const string Default = GroupName + ".Storelocation";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Storearea
    {
        public const string Default = GroupName + ".Storearea";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Supplier
    {
        public const string Default = GroupName + ".Supplier";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(WarehousePermissions));
    }
}
