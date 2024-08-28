using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物查询
/// </summary>
public class CargoSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 货物条码
    /// </summary>
    public string? Bn { get; set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public string? Sn { get; set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 货物类别名称
    /// </summary>
    public string? CategoryName { get; set; }

}
