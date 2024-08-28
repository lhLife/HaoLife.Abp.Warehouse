using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存查询入参
/// </summary>
[Serializable]
public class StockSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 货物名称
    /// </summary>
    public string? CargoName { get; set; }

    /// <summary>
    /// 货物条码
    /// </summary>
    public string? CargoBn { get; set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public string? CargoSn { get; set; }

    /// <summary>
    /// 货物id
    /// </summary>
    public Guid? CargoId { get; set; }

    /// <summary>
    /// 仓库编号
    /// </summary>
    public Guid? StorehouseId { get; set; }

    /// <summary>
    /// 库区编号
    /// </summary>
    public Guid? StoreareaId { get; set; }

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
