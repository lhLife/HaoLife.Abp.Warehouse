using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse;

public class CommonConsts
{
    public static int PrecisionLength { get; set; } = 11;
    public static int ScaleLength { get; set; } = 11;


    public static string ArrivedOrderNoDefaultTemplate => """A{{ date.now | date.to_string '%y%m%d%H%M%S' }}{{ r | string.slice 3}}""";

}
