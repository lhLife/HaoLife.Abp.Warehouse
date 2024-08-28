﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorelocationSearchDto : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 库区名称
    /// </summary>
    public string? StoreareaName { get; set; }

    /// <summary>
    /// 库区类型
    /// </summary>
    public StoreareaType? StoreareaType { get; set; }
    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid? StorehouseId { get; set; }

    /// <summary>
    /// 仓库名称
    /// </summary>
    public string? StorehouseName { get; set; }

    /// <summary>
    /// 库位编号
    /// </summary>
    public string? Code { get; set; }
}