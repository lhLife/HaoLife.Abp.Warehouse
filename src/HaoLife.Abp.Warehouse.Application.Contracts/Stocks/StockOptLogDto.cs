using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存操作日志
/// </summary>
public class StockOptLogDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物信息
    /// </summary>
    public CargoListDto Cargo { get; set; }

    /// <summary>
    /// 库存id
    /// </summary>
    public Guid StockId { get; set; }

    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 操作数量
    /// </summary>
    public int OptNumber { get; set; }

    /// <summary>
    /// 当前数量
    /// </summary>
    public int CurrentNumber { get; set; }


    /// <summary>
    /// 结果数量
    /// </summary>
    public int ResultNumber { get; set; }

    /// <summary>
    /// 库存操作类型
    /// </summary>
    public StockOptType StockOptType { get; set; }

}
