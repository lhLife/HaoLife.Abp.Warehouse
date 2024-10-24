﻿namespace HaoLife.Abp.Warehouse;

public static class WarehouseDbProperties
{
    public static string DbTablePrefix { get; set; } = "Abp";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "Warehouse";
}
