using HaoLife.Abp.Warehouse.Suppliers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Cargos;

/// <summary>
/// 货物明细
/// </summary>
public class CargoDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 货物图片
    /// </summary>
    public string Images { get; set; }

    /// <summary>
    /// 货物条码
    /// </summary>
    public string Bn { get; set; }

    /// <summary>
    /// 货物编码
    /// </summary>
    public string Sn { get; set; }

    /// <summary>
    /// 重量
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit { get; set; }


    /// <summary>
    /// 规格描述
    /// </summary>
    public string SpecDesc { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; set; }

    /// <summary>
    /// 货物类别
    /// </summary>
    public CargoCategoryDto? Category { get; set; }

    /// <summary>
    /// 供应商
    /// </summary>
    public SupplierDto? Supplier { get; set; }
}
