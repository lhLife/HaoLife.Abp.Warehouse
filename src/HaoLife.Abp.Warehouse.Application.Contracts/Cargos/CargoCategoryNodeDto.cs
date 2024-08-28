using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物类型节点
/// </summary>
public class CargoCategoryNodeDto : ExtensibleCreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物类别名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 是否有子节点
    /// </summary>
    public bool IsHaveChild { get; set; }

    /// <summary>
    /// 子类别
    /// </summary>
    public List<CargoCategoryNodeDto>? Childrens { get; set; }

    /// <summary>
    /// 所属类别id
    /// </summary>
    [JsonIgnore]
    public Guid? ParentId { get; set; }

}
