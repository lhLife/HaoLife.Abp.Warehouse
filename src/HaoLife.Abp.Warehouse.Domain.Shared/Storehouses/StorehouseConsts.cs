using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorehouseConsts
{
    public static int MaxNameLength { get; set; } = 64;
    public static int MaxAdcodeLength { get; set; } = 6;
    public static int MaxCityLength { get; set; } = 32;
    public static int MaxFullAddressLength { get; set; } = 1024;
    public static int MaxEmailLength { get; set; } = 64;
    public static int MaxPhoneLength { get; set; } = 11;
    public static int MaxLiaisonsLength { get; set; } = 32;
}
