using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderPickDto
{
    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 分拣数量
    /// </summary>
    public int Number { get; set; }


    /// <summary>
    /// 库存序列号
    /// </summary>
    public List<string>? SeriesNumbers { get; set; }
}
