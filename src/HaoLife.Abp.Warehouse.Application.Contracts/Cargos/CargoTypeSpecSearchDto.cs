using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物规格查询
/// </summary>
public class CargoTypeSpecSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 规格名称
    /// </summary>
    public string? Name { get; set; }
}
