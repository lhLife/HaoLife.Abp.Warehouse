using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Dispatchs;

/// <summary>
/// 发货单
/// </summary>
public class DispatchOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
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
    /// 发货单号
    /// </summary>
    public required string DispatchOrderNo { get; set; }


    /// <summary>
    /// 总数量
    /// </summary>
    public int TotalNumber { get; set; }

    /// <summary>
    /// 总重量
    /// </summary>
    public decimal TotalWeight { get; set; }


    /// <summary>
    /// 发货状态
    /// </summary>
    public DispatchStatus Stauts { get; set; }

    /// <summary>
    /// 打包人
    /// </summary>
    public string PackOperater { get; set; }

    /// <summary>
    /// 打包时间
    /// </summary>
    public DateTime? PackTime { get; set; }

    /// <summary>
    /// 称重人
    /// </summary>
    public string MeteringOperater { get; set; }

    /// <summary>
    /// 称重时间
    /// </summary>
    public DateTime? MeteringTime { get; set; }

}
