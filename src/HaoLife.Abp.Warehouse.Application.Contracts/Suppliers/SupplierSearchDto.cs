using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Suppliers;

public class SupplierSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 供应商名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? Liaisons { get; set; }
}
