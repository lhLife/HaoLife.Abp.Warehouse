using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存信息
/// </summary>
public class StockDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物信息
    /// </summary>
    public CargoListDto? Cargo { get; set; }

    /// <summary>
    /// 货物id
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

    /// <summary>
    /// 是否冻结
    /// </summary>
    public bool IsFreeze { get; set; }
}
