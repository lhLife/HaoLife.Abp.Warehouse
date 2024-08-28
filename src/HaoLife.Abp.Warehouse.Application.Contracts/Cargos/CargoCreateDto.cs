using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 创建货物
/// </summary>
public class CargoCreateDto : ExtensibleObject
{
    /// <summary>
    /// 货物名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 货物图片
    /// </summary>
    public required string Images { get; set; }

    /// <summary>
    /// 货物条码
    /// </summary>
    public required string Bn { get; set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public required string Sn { get; set; }

    /// <summary>
    /// 重量
    /// </summary>
    public decimal? Weight { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    public List<CargoSpecItemCreateDto>? Specs { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; set; }

    /// <summary>
    /// 货物类别id
    /// </summary>
    public Guid? CategoryId { get; set; }
    /// <summary>
    /// 供应商id
    /// </summary>
    public Guid? SupplierId { get; set; }
}
