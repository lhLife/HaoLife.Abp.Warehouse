using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 创建货物规格
/// </summary>
public class CargoTypeSpecCreateDto : ExtensibleObject
{
    /// <summary>
    /// 规格名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 规格值
    /// </summary>
    [Length(1, int.MaxValue)]
    public List<string> Values { get; set; }
}
