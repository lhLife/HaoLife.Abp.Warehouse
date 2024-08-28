using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.StockProcesses;

/// <summary>
/// 库存操作状态
/// </summary>
[Description("库存操作状态")]
public enum StockProcessStatus
{
    /// <summary>
    /// 待加工
    /// </summary>
    [Description("待加工")]
    UnProcess = 0,

    /// <summary>
    /// 已加工
    /// </summary>
    [Description("已加工")]
    Processed = 1,

    /// <summary>
    /// 已入库
    /// </summary>
    [Description("已入库")]
    Stocked = 1,
}
