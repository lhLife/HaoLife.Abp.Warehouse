using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 到货单号
    /// </summary>
    public string? OrderNo { get; set; }

    /// <summary>
    /// 开始预计到达时间
    /// </summary>
    public DateOnly? BeginExpectArrivedDate { get; set; }

    /// <summary>
    /// 截至预计到达时间
    /// </summary>
    public DateOnly? EndExpectArrivedDate { get; set; }
    /// <summary>
    /// 批次号
    /// </summary>
    public string? BatchNo { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string? Contacts { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    public string? ContactsPhone { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Memo { get; set; }

}
