using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.StockProcesses;

/// <summary>
/// 库存加工类型
/// </summary>
[Description("库存加工类型")]
public enum StockProcessType
{
    /// <summary>
    /// 拆分
    /// </summary>
    [Description("拆分")]
    Split = 0,

    /// <summary>
    /// 组合
    /// </summary>
    [Description("组合")]
    Compose = 1,
}
