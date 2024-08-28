using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 存储工具属性值
/// </summary>
public class StoreToolAttrValueCreateDto
{
    /// <summary>
    /// 存储工具属性名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 存储工具属性值
    /// </summary>
    public string Value { get; set; }
}
