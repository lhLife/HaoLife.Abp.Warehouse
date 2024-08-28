using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.StockFreezes;

/// <summary>
/// 库存冻结
/// </summary>
public class StockFreeze : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }
    /// <summary>
    /// 实体版本
    /// </summary>
    public int EntityVersion { get; protected set; }


    /// <summary>
    /// 作业编号
    /// </summary>
    public string JobCode { get; set; }

    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }


    /// <summary>
    /// 操作人
    /// </summary>
    public string Operater { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime? OperateTime { get; set; }
}
