using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StoreToolSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }
}
