﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 到货单号
    /// </summary>
    public string OrderNo { get; set; }

    /// <summary>
    /// 预计到达时间
    /// </summary>
    public DateOnly? ExpectArrivedDate { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    public string BatchNo { get; set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string Contacts { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    public string ContactsPhone { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Memo { get; set; }


    /// <summary>
    /// 到达时间
    /// </summary>
    public DateTime? ArrivedDate { get; set; }

    /// <summary>
    /// 卸货时间
    /// </summary>
    public DateTime? UnloadTime { get; set; }

    /// <summary>
    /// 卸货操作人
    /// </summary>
    public string UnloadOperator { get; set; }
    /// <summary>
    /// 到货状态
    /// </summary>
    public ArrivedStatus Status { get; set; }

    /// <summary>
    /// 到货明细
    /// </summary>
    public List<ArrivedOrderItemDto> Items { get; set; }
}


