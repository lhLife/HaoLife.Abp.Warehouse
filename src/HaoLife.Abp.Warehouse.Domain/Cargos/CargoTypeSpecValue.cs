using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类型规格值
/// </summary>
public class CargoTypeSpecValue : FullAuditedEntity<Guid>, IMultiTenant
{
    protected CargoTypeSpecValue()
    {
        
    }

    public CargoTypeSpecValue(Guid id, string value, int sort, Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.Value = value;
        this.Sort = sort;
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 规格值
    /// </summary>
    public string Value { get; protected set; }

    /// <summary>
    /// 值排序
    /// </summary>
    public int Sort { get; protected set; }
}
