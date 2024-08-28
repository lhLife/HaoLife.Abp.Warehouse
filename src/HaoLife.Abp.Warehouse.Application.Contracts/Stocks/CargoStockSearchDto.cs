using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 货物库存查询入参
/// </summary>
[Serializable]
public class CargoStockSearchDto : PagedAndSortedResultRequestDto
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

}


