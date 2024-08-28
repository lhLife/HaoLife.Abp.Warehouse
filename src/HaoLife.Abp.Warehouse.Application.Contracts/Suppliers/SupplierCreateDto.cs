﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HaoLife.Abp.Warehouse.Suppliers;

/// <summary>
/// 创建供货商
/// </summary>
public class SupplierCreateDto
{
    /// <summary>
    /// 供应商名称
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
}
