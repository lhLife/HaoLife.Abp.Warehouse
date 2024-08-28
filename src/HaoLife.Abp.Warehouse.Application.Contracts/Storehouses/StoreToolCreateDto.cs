using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StoreToolCreateDto : ExtensibleObject
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }


    /// <summary>
    /// 属性名
    /// </summary>
    [Length(1, int.MaxValue)]
    public List<string> Attrs { get; set; }
}
