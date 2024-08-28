using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorehouseSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 仓库名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 城市行政编码
    /// </summary>
    public string? Adcode { get; set; }

    /// <summary>
    /// 城市名称
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? Liaisons { get; set; }
}
