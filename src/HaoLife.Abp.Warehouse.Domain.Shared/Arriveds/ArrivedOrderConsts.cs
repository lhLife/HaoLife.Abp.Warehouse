using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderConsts
{
    public static int MaxContactsLength { get; set; } = 64;
    public static int MaxContactsPhoneLength { get; set; } = 11;
    //Memo
    public static int MaxMemoLength { get; set; } = 1024;

    public static int MaxUnloadOperatorLength { get; set; } = 64;
    
}
