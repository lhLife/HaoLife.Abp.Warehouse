using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作入参
/// </summary>
[Serializable]
public class StockInvokeDto
{
    /// <summary>
    /// 货物id
    /// </summary>
    public required Guid CargoId { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    [Range(1, int.MaxValue)]
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
}
