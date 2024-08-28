using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类别
/// </summary>
public class CargoCategory : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected CargoCategory()
    {

    }
    public CargoCategory(Guid id, string name, Guid? parentId, Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetName(name);
        this.SetParentId(parentId);
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; set; }

    /// <summary>
    /// 货物类别名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 所属类别id
    /// </summary>
    public Guid? ParentId { get; set; }


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), CargoCategoryConsts.MaxNameLength);
    }

    public virtual void SetParentId(Guid? parentId)
    {
        this.ParentId = parentId;
    }
}
