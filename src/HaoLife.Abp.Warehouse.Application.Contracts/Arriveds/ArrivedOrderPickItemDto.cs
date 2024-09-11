using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderPickItemDto : EntityDto<Guid>
{
    /// <summary>
    /// 库存序列号
    /// </summary>
    public string? SeriesNumber { get; set; }

    /// <summary>
    /// 分拣数量
    /// </summary>
    public int Number { get; set; }


    /// <summary>
    /// 已入库数
    /// </summary>
    public int StockNumber { get; set; }

    /// <summary>
    /// 库位id
    /// </summary>
    public Guid? StorelocationId { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public string? StorelocationCode { get; set; }
}
