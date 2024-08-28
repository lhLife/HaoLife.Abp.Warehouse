using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货状态
/// </summary>
[Description("到货状态")]
public enum ArrivedStatus
{
    /// <summary>
    /// 待到货
    /// </summary>
    [Description("待到货")]
    PreArrived = 0,

    /// <summary>
    /// 待卸货
    /// </summary>
    [Description("待卸货")]
    ToBeUnload = 1,
    /// <summary>
    /// 待分拣
    /// </summary>
    [Description("待分拣")]
    UnSorted = 2,

    /// <summary>
    /// 待上架
    /// </summary>
    [Description("待上架")]
    UnPutAway = 3,
    /// <summary>
    /// 已完成
    /// </summary>
    [Description("已完成")]
    Completed = 4,

    /// <summary>
    /// 已取消
    /// </summary>
    [Description("已取消")]
    Canceled = 9,
}