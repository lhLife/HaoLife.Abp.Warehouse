using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 货物库存
/// </summary>
public class CargoStockDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 货物信息
    /// </summary>
    public CargoListDto Cargo { get; set; }

    /// <summary>
    /// 库存数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 可用数量
    /// </summary>
    public int UsableNumber { get; set; }

    /// <summary>
    /// 锁定数量
    /// </summary>
    public int LockNumber { get; set; }

    /// <summary>
    /// 冻结数量
    /// </summary>
    public int FreezeNumber { get; set; }


    /// <summary>
    /// 待到货数
    /// </summary>
    public int PreArrivedNumber { get; set; }

    /// <summary>
    /// 待卸货数
    /// </summary>
    public int ToBeUnloadNumber { get; set; }

    /// <summary>
    /// 待分拣数
    /// </summary>
    public int UnSortedNumber { get; set; }

    /// <summary>
    /// 待上架数
    /// </summary>
    public int UnPutAwayNumber { get; set; }
}
