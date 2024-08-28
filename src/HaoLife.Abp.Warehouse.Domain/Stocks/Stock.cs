using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Storehouses;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存
/// </summary>
public class Stock : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    protected Stock()
    {

    }

    public Stock(Guid id, Guid cargoId, string cargoSn, Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetCargoId(cargoId);
        this.SetCargoSn(cargoSn);

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
    /// 货物id
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public string CargoSn { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 库位id
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 库区id
    /// </summary>
    public Guid? StoreareaId { get; set; }
    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid? StorehouseId { get; set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }

    /// <summary>
    /// 是否冻结
    /// </summary>
    public bool IsFreeze { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

    public virtual void SetCargoId(Guid cargoId)
    {
        this.CargoId = cargoId;
    }

    public virtual void SetCargoSn(string cargoSn)
    {
        this.CargoSn = cargoSn;
    }

    public virtual void AddNumber(int number)
    {
        this.Number += number;
    }

    public virtual void DeductNumber(int number)
    {
        if (this.Number - number < 0) throw new BusinessException();

        this.Number -= number;
    }

    public virtual void SetStorelocation(Guid? storehouseId, Guid? storeareaId, Guid? storelocationId)
    {
        this.StorehouseId = storehouseId;
        this.StoreareaId = storeareaId;
        this.StorelocationId = storelocationId;
    }

    public virtual void SetSeriesNumber(string? seriesNumber)
    {
        this.SeriesNumber = Check.NotNullOrWhiteSpace(seriesNumber, nameof(seriesNumber), StockConsts.MaxSeriesNumberLength); ;
    }

    public virtual void SetBatchNo(string? batchNo)
    {
        this.BatchNo = Check.NotNullOrWhiteSpace(batchNo, nameof(batchNo), StockConsts.MaxBatchNoLength);
    }


    public virtual void HandleFreeze(bool isFreeze)
    {
        if (this.IsFreeze == isFreeze) throw new BusinessException();

        this.IsFreeze = isFreeze;
    }

}
