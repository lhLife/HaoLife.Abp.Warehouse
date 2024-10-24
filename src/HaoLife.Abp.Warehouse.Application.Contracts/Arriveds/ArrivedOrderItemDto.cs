﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Arriveds;

public class ArrivedOrderItemDto : EntityDto<Guid>
{
    /// <summary>
    /// 货物编号
    /// </summary>
    public Guid CargoId { get; set; }

    /// <summary>
    /// 货物名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 货物条码 - BarCode
    /// </summary>
    public string Bn { get; set; }

    /// <summary>
    /// 货物编码 - SerialNo
    /// </summary>
    public string Sn { get; set; }

    /// <summary>
    /// 规格描述 - 颜色:红,长:42,条纹:蓝金
    /// </summary>
    public string SpecDesc { get; set; }


    /// <summary>
    /// 到货数量
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// 货物成本单价
    /// </summary>
    public decimal? CostPrice { get; set; }




    /// <summary>
    /// 分拣数量
    /// </summary>
    public int PickNumber { get; set; }

    /// <summary>
    /// 已入库数
    /// </summary>
    public int StockedNumber { get; set; }

    /// <summary>
    /// 分拣明细
    /// </summary>
    public List<ArrivedOrderPickItemDto> Picks { get; set; }
}
