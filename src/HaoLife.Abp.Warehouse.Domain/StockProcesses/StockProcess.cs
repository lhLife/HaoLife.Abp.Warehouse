using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.StockProcesses;

/// <summary>
/// 库存加工
/// </summary>
public class StockProcess : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
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
    /// 库存加工类型
    /// </summary>
    public StockProcessType Type { get; set; }

    /// <summary>
    /// 库存加工状态
    /// </summary>
    public StockProcessStatus Status { get; set; }

    /// <summary>
    /// 操作人
    /// </summary>
    public string Operater { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime? OperateTime { get; set; }


    /// <summary>
    /// 库存加工项
    /// </summary>
    public List<StockProcessItem> Items { get; set; }
}
