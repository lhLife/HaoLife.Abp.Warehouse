using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Cargos;

public class CargoConsts
{
    public static int MaxNameLength { get; set; } = 64;
    public static int MaxImagesLength { get; set; } = 2048;

    public static int MaxCodeLength { get; set; } = 64;
    public static int MaxBnLength { get; set; } = MaxCodeLength;
    public static int MaxSnLength { get; set; } = MaxCodeLength;
    public static int MaxUnitLength { get; set; } = 32;
    public static int MaxSpecDescLength { get; set; } = 1024;
}
