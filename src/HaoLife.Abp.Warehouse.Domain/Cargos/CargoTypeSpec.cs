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
/// 货物类型规格
/// </summary>
public class CargoTypeSpec : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected CargoTypeSpec()
    {
        
    }

    public CargoTypeSpec(Guid id, string name, int sort, Guid? tenantId = null)
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
    /// 规格名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; protected set; }

    /// <summary>
    /// 货物规格值
    /// </summary>

    public virtual List<CargoTypeSpecValue> Values { get; protected set; }



    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), CargoTypeSpecConsts.MaxNameLength);
    }

    public virtual void SetSort(int sort)
    {
        this.Sort = sort;
    }

    public virtual void AddValue(Guid id, string value, int sort)
    {
        if (this.Values == null) this.Values = new List<CargoTypeSpecValue>();

        this.Values.Add(new CargoTypeSpecValue(id, value, sort, this.TenantId));
    }
}
