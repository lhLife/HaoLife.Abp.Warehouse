using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.StockProcesses;

/// <summary>
/// 库存加工项
/// </summary>
public class StockProcessItem : AuditedEntity<Guid>, IMultiTenant
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
    /// 操作数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 是否来源
    /// </summary>
    public bool IsSource { get; set; }

    /// <summary>
    /// 库位id
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }
}
