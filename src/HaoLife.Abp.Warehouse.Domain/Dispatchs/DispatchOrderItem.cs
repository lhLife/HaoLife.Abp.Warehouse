using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Dispatchs;

/// <summary>
/// 发货单明细
/// </summary>
public class DispatchOrderItem : FullAuditedEntity<Guid>, IMultiTenant
{
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 发货数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 锁定数量
    /// </summary>
    public int LockNumber { get; set; }


    /// <summary>
    /// 已选数量
    /// </summary>
    public int PickedNumber { get; set; }
}
