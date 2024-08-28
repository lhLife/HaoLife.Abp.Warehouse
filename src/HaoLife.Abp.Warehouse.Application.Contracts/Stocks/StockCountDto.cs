using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Stocks;

internal class StockCountDto
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
    /// 冻结数量
    /// </summary>
    public int FreezeNumber { get; set; }
}
