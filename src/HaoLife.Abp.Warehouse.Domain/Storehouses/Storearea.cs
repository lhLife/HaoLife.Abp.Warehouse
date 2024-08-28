using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 库区
/// </summary>
public class Storearea : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected Storearea()
    {

    }
    public Storearea(Guid id, string name, StoreareaType storeareaType, bool isEnable
        , Guid storehouseId
        , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetName(name);
        this.SetStoreareaType(storeareaType);
        this.SetEnable(isEnable);
        this.SetStorehouseId(storehouseId);

    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 库区名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 库区类型
    /// </summary>
    public StoreareaType StoreareaType { get; protected set; }


    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; protected set; }

    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid StorehouseId { get; protected set; }

    ///// <summary>
    ///// 仓库
    ///// </summary>
    //public virtual Storehouse Storehouse { get; protected set; }


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), StoreareaConsts.MaxNameLength);
    }
    public virtual void SetStoreareaType(StoreareaType storeareaType)
    {
        this.StoreareaType = storeareaType;
    }
    public virtual void SetEnable(bool isEnable)
    {
        this.IsEnable = isEnable;
    }

    public virtual void SetStorehouseId(Guid storehouseId)
    {
        this.StorehouseId = storehouseId;
    }

    //public virtual void SetStorehouse(Storehouse storehouse)
    //{
    //    this.StorehouseId = storehouse.Id;
    //    this.Storehouse = storehouse;
    //}
}
