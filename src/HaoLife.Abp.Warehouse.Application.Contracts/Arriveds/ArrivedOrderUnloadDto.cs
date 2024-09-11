using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderUnloadDto
{
    /// <summary>
    /// 卸货时间
    /// </summary>
    public DateTime UnloadTime { get; set; }

    /// <summary>
    /// 卸货操作人
    /// </summary>
    public string? UnloadOperator { get; set; }
}
