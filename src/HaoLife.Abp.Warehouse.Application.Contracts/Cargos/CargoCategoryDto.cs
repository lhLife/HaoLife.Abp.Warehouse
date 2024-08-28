using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类别
/// </summary>
public class CargoCategoryDto : ExtensibleCreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物类别名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 所属类别id
    /// </summary>
    public Guid? ParentId { get; set; }
}
