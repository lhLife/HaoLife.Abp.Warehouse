using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorelocationCreateDto: ExtensibleObject
{
    /// <summary>
    /// 库位编号
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 库区id
    /// </summary>
    public Guid StoreareaId { get; set; }
    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid StorehouseId { get; set; }

    /// <summary>
    /// 存储工具id
    /// </summary>
    public Guid? StoreToolId { get; set; }

    /// <summary>
    /// 存储工具属性值
    /// </summary>
    public List<StoreToolAttrValueCreateDto> StoreToolAttrs { get; set; }
}
