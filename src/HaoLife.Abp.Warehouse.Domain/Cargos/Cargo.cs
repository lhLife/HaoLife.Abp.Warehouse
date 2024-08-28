
using System;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Auditing;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Suppliers;
using Volo.Abp;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物
/// </summary>
/// <remarks>
/// 商品货物 - Product
/// 库存货物 - Cargo
/// 库存货物在仓储领域中，没有销售属性，他可以是内部耗品、礼品、赠品、货物等
/// 商品货物在销售领域中，他是商品中一个销售项，商品变成一个虚拟的集合，实际销售的是货品。
/// 如：荔枝礼盒和荔枝A品，在商品上他们是同一个商品，但在实际购买时，用户选择的是其中一个货品，他们的售价也不一样。
/// </remarks>
public class Cargo : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    protected Cargo()
    {

    }

    public Cargo(Guid id
        , [Required] string name
        , [Required] string images
        , [Required] string bn
        , [Required] string sn
        , bool isEnable
        , decimal? weight = null
        , string? unit = null
        , string? specDesc = null
        , decimal? costPrice = null
        , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetName(name);
        this.SetImages(images);
        this.SetBn(bn);
        this.SetSn(sn);
        this.SetEnable(isEnable);
        this.SetWeight(weight);
        this.SetUnit(unit);
        this.SetSpecDesc(specDesc);
        this.SetCostPrice(costPrice);

    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 实体版本
    /// </summary>
    public int EntityVersion { get; protected set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 货物图片
    /// </summary>
    public string Images { get; protected set; }

    /// <summary>
    /// 货物条码
    /// </summary>
    public string Bn { get; protected set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public string Sn { get; protected set; }

    /// <summary>
    /// 重量
    /// </summary>
    public decimal? Weight { get; protected set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string? Unit { get; protected set; }


    /// <summary>
    /// 规格描述
    /// </summary>
    public string? SpecDesc { get; protected set; }


    ///// <summary>
    ///// 库存
    ///// </summary>
    //public int Stock { get; set; }

    ///// <summary>
    ///// 冻结库存
    ///// </summary>
    //public int FreezeStock { get; set; }

    //到货通知数，待卸货数，待分拣数，已分拣数

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; protected set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; protected set; }

    /// <summary>
    /// 货物类别id
    /// </summary>
    public Guid? CategoryId { get; protected set; }
    /// <summary>
    /// 供应商id
    /// </summary>
    public Guid? SupplierId { get; protected set; }

    ///// <summary>
    ///// 货物类别
    ///// </summary>
    //public CargoCategory? Category { get; set; }

    ///// <summary>
    ///// 供应商
    ///// </summary>
    //public Supplier? Supplier { get; set; }



    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), CargoConsts.MaxNameLength);
    }

    public virtual void SetImages(string? images)
    {
        this.Images = Check.NotNullOrWhiteSpace(images, nameof(images), CargoConsts.MaxImagesLength);
    }

    public virtual void SetBn(string bn)
    {
        this.Bn = Check.NotNullOrWhiteSpace(bn, nameof(bn), CargoConsts.MaxBnLength);
    }
    public virtual void SetSn(string sn)
    {
        this.Sn = Check.NotNullOrWhiteSpace(sn, nameof(sn), CargoConsts.MaxSnLength);
    }
    public virtual void SetEnable(bool isEnable)
    {
        this.IsEnable = isEnable;

    }
    public virtual void SetWeight(decimal? weight)
    {
        this.Weight = weight;
    }
    public virtual void SetUnit(string? unit)
    {
        this.Unit = unit;
    }
    public virtual void SetSpecDesc(string? specDesc)
    {
        this.SpecDesc = specDesc;
    }
    public virtual void SetCostPrice(decimal? costPrice)
    {
        this.CostPrice = costPrice;
    }

    public virtual void SetCategoryId(Guid? categoryId)
    {
        this.CategoryId = categoryId;
    }
    public virtual void SetSupplierId(Guid? supplierId)
    {
        this.SupplierId = supplierId;
    }
}
