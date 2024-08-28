using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.ObjectExtending;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorehouseCreateDto: ExtensibleObject
{
    /// <summary>
    /// 仓库名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 城市行政编码
    /// </summary>
    public string Adcode { get; set; }

    /// <summary>
    /// 城市名称
    /// </summary>
    public string City { get; set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    public string FullAddress { get; set; }

    /// <summary>
    /// 邮件
    /// </summary>
    public string Email { get; set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string Liaisons { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }
}
