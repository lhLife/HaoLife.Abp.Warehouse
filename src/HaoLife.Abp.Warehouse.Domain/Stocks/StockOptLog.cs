using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作记录
/// </summary>
public class StockOptLog : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected StockOptLog()
    {

    }
    public StockOptLog(Guid id, Stock result, int optNumber, int currentNumber, int resultNumber, StockOptType stockOptType, Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.StockId = result.Id;
        this.CargoId = result.CargoId;
        this.StorelocationId = result.StorelocationId;
        this.OptNumber = optNumber;
        this.CurrentNumber = currentNumber;
        this.ResultNumber = resultNumber;
    }
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 库存id
    /// </summary>
    public Guid StockId { get; set; }

    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 操作数量
    /// </summary>
    public int OptNumber { get; set; }

    /// <summary>
    /// 当前数量
    /// </summary>
    public int CurrentNumber { get; set; }


    /// <summary>
    /// 结果数量
    /// </summary>
    public int ResultNumber { get; set; }

    /// <summary>
    /// 库存操作类型
    /// </summary>
    public StockOptType StockOptType { get; set; }

}
