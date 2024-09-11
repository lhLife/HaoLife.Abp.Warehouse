using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderStockDto
{
    /// <summary>
    /// 到货项id
    /// </summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// 到货分拣项id
    /// </summary>
    public Guid PickItemId { get; set; }


    /// <summary>
    /// 库位id
    /// </summary>
    public Guid StorelocationId { get; set; }
}
