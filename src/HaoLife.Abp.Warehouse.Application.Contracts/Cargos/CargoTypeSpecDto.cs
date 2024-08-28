using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物规格
/// </summary>
public class CargoTypeSpecDto : ExtensibleAuditedEntityDto<Guid>
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
    public List<string> Values { get; set; }
}
