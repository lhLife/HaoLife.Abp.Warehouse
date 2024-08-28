using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货单货物明细
/// </summary>
public class ArrivedOrderItem : FullAuditedEntity<Guid>, IMultiTenant
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
    /// 货物名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 货物条码 - BarCode
    /// </summary>
    public required string Bn { get; set; }

    /// <summary>
    /// 货物编码 - SerialNo
    /// </summary>
    public required string Sn { get; set; }

    /// <summary>
    /// 规格描述 - 颜色:红,长:42,条纹:蓝金
    /// </summary>
    public string SpecDesc { get; set; }


    /// <summary>
    /// 到货数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; set; }



    /// <summary>
    /// 分拣数量
    /// </summary>
    public int SortNumber { get; set; }

    /// <summary>
    /// 已入库数
    /// </summary>
    public int StockedNumber { get; set; }

    /// <summary>
    /// 分拣记录
    /// </summary>
    public List<ArrivedOrderSortItem> Sorts { get; set; }
}


