using HaoLife.Abp.Warehouse.Cargos;
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
/// 存储工具
/// </summary>
public class StoreTool : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected StoreTool()
    {

    }
    public StoreTool(Guid id, string name, int sort, Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetName(name);
        this.SetSort(sort);
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; protected set; }


    /// <summary>
    /// 存储工具属性
    /// </summary>
    public virtual List<StoreToolAttr> Attrs { get; protected set; }


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), StoreToolConsts.MaxNameLength);
    }

    public virtual void SetSort(int sort)
    {
        this.Sort = sort;
    }

    public virtual void AddAttr(Guid id, string name, int sort)
    {
        if (this.Attrs == null) this.Attrs = new List<StoreToolAttr>();

        this.Attrs.Add(new StoreToolAttr(id, name, sort, this.TenantId));
    }
}
