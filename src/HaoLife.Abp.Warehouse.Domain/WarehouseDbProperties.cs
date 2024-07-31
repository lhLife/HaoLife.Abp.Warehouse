namespace HaoLife.Abp.Warehouse;

public static class WarehouseDbProperties
{
    public static string DbTablePrefix { get; set; } = "Warehouse";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Warehouse";
}
