using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 存储工具属性
/// </summary>
public class StoreToolAttr : Entity<Guid>, IMultiTenant
{
    protected StoreToolAttr()
    {

    }
    public StoreToolAttr(Guid id, string name, int sort, Guid? tenantId = null)
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
    /// 属性名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; protected set; }


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), StoreToolAttrConsts.MaxNameLength);
    }

    public virtual void SetSort(int sort)
    {
        this.Sort = sort;
    }
}
