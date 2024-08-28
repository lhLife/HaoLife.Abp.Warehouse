using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货单货物分拣明细
/// </summary>
public class ArrivedOrderSortItem : FullAuditedEntity<Guid>, IMultiTenant
{
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string SeriesNumber { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Seq { get; set; }

    /// <summary>
    /// 分拣数量
    /// </summary>
    public int Number { get; set; }


    /// <summary>
    /// 已入库数
    /// </summary>
    public int StockNumber { get; set; }

    /// <summary>
    /// 是否入库
    /// </summary>
    public bool IsStock { get; set; }



    //过期日期：货物的过期时间，过期后不能发货。
}
