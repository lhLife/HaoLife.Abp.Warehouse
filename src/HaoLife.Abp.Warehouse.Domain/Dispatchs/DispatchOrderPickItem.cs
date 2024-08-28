using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Dispatchs;

/// <summary>
/// 发货单拣货明细
/// </summary>
public class DispatchOrderPickItem : FullAuditedEntity<Guid>, IMultiTenant
{
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 库位id
    /// </summary>
    public Guid StorelocationId { get; set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }

    /// <summary>
    /// 挑选数量
    /// </summary>
    public int PickNumber { get; set; }

    /// <summary>
    /// 已选数量
    /// </summary>
    public int PickedNumber { get; set; }

    /// <summary>
    /// 是否同步库存
    /// </summary>
    public bool IsStock { get; set; }
}
