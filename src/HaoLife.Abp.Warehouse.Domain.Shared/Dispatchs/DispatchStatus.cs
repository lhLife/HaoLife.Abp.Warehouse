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
    /// <remarks>
    /// 新建发货单，生成要发货的货物和数量
    /// </remarks>
    [Description("预发货")]
    PreDispatch = 0,

    /// <summary>
    /// 待发货
    /// </summary>
    /// <remarks>
    /// 确认发货单中发货的货物（待配货）（锁库存）
    /// 从仓库中(库位)进行配货，列出配货的待选项，提供给用户确认，用户可以从待选项中选择其他仓库（库位）的货物进行配货
    /// </remarks>
    [Description("待发货")]
    UnDispatch = 1,
    /// <summary>
    /// 待拣货
    /// </summary>
    /// <remarks>
    /// 确认配货明细（已配货）,确认用户已经确认选取的配货明细
    /// </remarks>
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
