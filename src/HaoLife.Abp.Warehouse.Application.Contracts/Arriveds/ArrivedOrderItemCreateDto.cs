using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderItemCreateDto
{
    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }


    /// <summary>
    /// 到货数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; set; }
}
