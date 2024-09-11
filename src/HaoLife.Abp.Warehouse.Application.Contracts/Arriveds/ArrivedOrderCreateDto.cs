using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 创建到货单
/// </summary>
public class ArrivedOrderCreateDto
{
    /// <summary>
    /// 预计到达时间
    /// </summary>
    public DateOnly? ExpectArrivedDate { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public required string BatchNo { get; set; }

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

    /// <summary>
    /// 到货明细
    /// </summary>
    [Length(1, int.MaxValue)]
    public required List<ArrivedOrderItemCreateDto> Items { get; set; }

}
