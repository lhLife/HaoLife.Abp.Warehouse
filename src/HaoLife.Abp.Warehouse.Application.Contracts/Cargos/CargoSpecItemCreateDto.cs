using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物规格项
/// </summary>
public class CargoSpecItemCreateDto
{
    /// <summary>
    /// 货物规格名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 货物规格值
    /// </summary>
    public string Value { get; set; }
}
