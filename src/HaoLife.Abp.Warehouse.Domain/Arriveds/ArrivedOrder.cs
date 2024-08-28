using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货单
/// </summary>
public class ArrivedOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 实体版本
    /// </summary>
    public int EntityVersion { get; protected set; }

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




    //到货的运输工具，如果是车辆，是否有车牌号（车头，挂车），联系人一般是司机还是货主，如果是货主，运输工具里面应该还有司机信息
    //运输工具有：大货车（车头，挂车），小货车（一个车牌）

    //分拣：可以对到货的货物进行一次次分拣，每次分拣出数量，分拣出的总数有可能少于总数（是否可以多于总数，可以多）
    //分拣时可以对每次分拣的货物生成一次分拣记录，或分拣时对每一个商品生成分拣记录
    //上架：可以对分拣记录进行上架，可对每个分拣记录多次上架（上架不同数量的货物到不同库位上），直到分拣的货物全部上架完成


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
    public List<ArrivedOrderItem> Itmes { get; set; }
}

