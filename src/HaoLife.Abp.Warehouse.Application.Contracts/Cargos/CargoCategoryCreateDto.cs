using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Validation;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 创建货物类别
/// </summary>
public class CargoCategoryCreateDto : ExtensibleObject
{
    public required string Name { get; set; }

    /// <summary>
    /// 父编号
    /// </summary>
    public Guid? ParentId { get; set; }
}
