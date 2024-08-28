using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作类型
/// </summary>
[Description("库存操作类型")]
public enum StockOptType
{
    /// <summary>
    /// 入库
    /// </summary>
    [Description("入库")]
    In = 1,
    /// <summary>
    /// 出库
    /// </summary>
    [Description("出库")]
    Out = 2,
    /// <summary>
    /// 盘盈
    /// </summary>
    [Description("盘盈")]
    CheckProfit = 3,
    /// <summary>
    /// 旁亏
    /// </summary>
    [Description("盘亏")]
    CheckLoss = 4,

    /// <summary>
    /// 冻结
    /// </summary>
    [Description("冻结")]
    Freeze = 5,

    /// <summary>
    /// 解冻
    /// </summary>
    [Description("解冻")]
    UnFreeze = 6,
}
