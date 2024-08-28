using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Dispatchs;

/// <summary>
/// 发货状态
/// </summary>
[Description("发货状态")]
public enum DispatchStatus
{
    /// <summary>
    /// 预发货
    /// </summary>
    [Description("预发货")]
    PreDispatch = 0,

    /// <summary>
    /// 待发货
    /// </summary>
    [Description("待发货")]
    UnDispatch = 1,
    /// <summary>
    /// 待拣货
    /// </summary>
    [Description("待拣货")]
    UnPicking = 2,

    /// <summary>
    /// 待打包
    /// </summary>
    [Description("待打包")]
    UnPacking = 3,
    /// <summary>
    /// 待称重
    /// </summary>
    [Description("待称重")]
    UnMetering = 4,

    /// <summary>
    /// 待出库
    /// </summary>
    [Description("待出库")]
    UnOuted = 5,
    /// <summary>
    /// 待签收
    /// </summary>
    [Description("待签收")]
    UnSigned = 6,

    /// <summary>
    /// 待打已签收包
    /// </summary>
    [Description("已签收")]
    Singed = 7,

    /// <summary>
    /// 已取消
    /// </summary>
    [Description("已取消")]
    Canceled = 9,
}
