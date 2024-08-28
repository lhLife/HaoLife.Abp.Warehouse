using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 库位
/// </summary>
public class Storelocation : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected Storelocation()
    {

    }
    public Storelocation(Guid id, string code, bool isEnable, Guid storehouseId, Guid storeareaId
        , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetCode(code);
        this.SetEnable(isEnable);
        this.SetStorehouseId(storehouseId);
        this.SetStoreareaId(storeareaId);
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public string Code { get; protected set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; protected set; }

    /// <summary>
    /// 库区id
    /// </summary>
    public Guid StoreareaId { get; protected set; }
    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid StorehouseId { get; protected set; }

    ///// <summary>
    ///// 库区
    ///// </summary>
    //public virtual Storearea Storearea { get; protected set; }

    ///// <summary>
    ///// 仓库
    ///// </summary>
    //public virtual Storehouse Storehouse { get; protected set; }

    /// <summary>
    /// 存储工具id
    /// </summary>
    public Guid? StoreToolId { get; protected set; }

    /// <summary>
    /// 存储工具属性描述
    /// </summary>
    public string StoreToolAttrDesc { get; protected set; }

    ///// <summary>
    ///// 存储工具
    ///// </summary>
    //public virtual StoreTool? StoreTool { get; set; }

    //仓库id-库区id-货架号[层号][位号]
    //14号楼仓库-01号货架-01号货架1层1格
    //14-s01-010101
    //14-s01-010201
    //14-s01-010203
    //14号楼仓库-09号收货货架-01(堆地上)
    //14-t09-01
    //存储工具规格：货架 (无规格=堆放)

    //仓库seq-库区seq-库位seq
    //仓库seq-库区seq-货架号-层号-位号

    public virtual void SetCode(string code)
    {
        this.Code = Check.NotNullOrWhiteSpace(code, nameof(code), StorelocationConsts.MaxCodeLength);
    }

    public virtual void SetEnable(bool enable)
    {
        this.IsEnable = enable;
    }

    public virtual void SetStorehouseId(Guid storehouseId)
    {
        this.StorehouseId = storehouseId;
    }
    public virtual void SetStoreareaId(Guid storeareaId)
    {
        this.StoreareaId = storeareaId;
    }

    //public virtual void SetStorehouse(Storehouse storehouse)
    //{
    //    //this.StorehouseId = storehouse.Id;
    //    this.Storehouse = storehouse;
    //}
    //public virtual void SetStorearea(Storearea storearea)
    //{
    //    //this.StoreareaId = storearea.Id;
    //    this.Storearea = storearea;
    //}

    public virtual void SetStoreToolAttrDesc(string storeToolAttrDesc)
    {
        this.StoreToolAttrDesc = Check.NotNullOrWhiteSpace(storeToolAttrDesc, nameof(storeToolAttrDesc), StorelocationConsts.MaxStoreToolSpecDescLength);
    }

}
