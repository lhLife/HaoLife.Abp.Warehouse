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
    protected ArrivedOrderItem()
    {

    }

    public ArrivedOrderItem(Guid id, Guid cargoId, string name, string bn, string sn, string? specDesc, int number, decimal? costPrice = null
         , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.CargoId = cargoId;
        this.Name = name;
        this.Bn = bn;
        this.Sn = sn;
        this.SpecDesc = specDesc;
        this.Number = number;
        this.CostPrice = costPrice;
    }

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
    public string Name { get; set; }

    /// <summary>
    /// 货物条码 - BarCode
    /// </summary>
    public string Bn { get; set; }

    /// <summary>
    /// 货物编码 - SerialNo
    /// </summary>
    public string Sn { get; set; }

    /// <summary>
    /// 规格描述 - 颜色:红,长:42,条纹:蓝金
    /// </summary>
    public string? SpecDesc { get; set; }


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
    public int PickNumber { get; set; }

    /// <summary>
    /// 已入库数
    /// </summary>
    public int StockedNumber { get; set; }

    /// <summary>
    /// 分拣记录
    /// </summary>
    public List<ArrivedOrderPickItem> Picks { get; set; } = new List<ArrivedOrderPickItem>();



    public virtual void AddPick(Guid id, string? seriesNumber, int number)
    {
        var seq = this.Picks.LastOrDefault()?.Seq ?? 1;
        this.Picks.Add(new ArrivedOrderPickItem(id, seriesNumber, seq, number, this.TenantId));

        this.PickNumber = this.Picks.Sum(a => a.Number);
    }


    public virtual void UpdateStockedNumber()
    {
        this.StockedNumber = this.Picks.Where(a => a.IsStock).Sum(a => a.StockNumber);
    }

    public virtual bool IsAllItemStock()
    {
        return this.Picks.Any(a => !a.IsStock);
    }
}


