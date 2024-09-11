using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货单
/// </summary>
public class ArrivedOrder : FullAuditedAggregateRoot<Guid>, IMultiTenant, IHasEntityVersion
{
    protected ArrivedOrder()
    {

    }
    public ArrivedOrder(Guid id, string orderNo, string batchNo, DateOnly? expectArrivedDate = null, string? contacts = null, string? contactsPhone = null, string? memo = null
          , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetOrderNo(orderNo);
        this.SetBatchNo(batchNo);
        this.SetExpectArrivedDate(expectArrivedDate);
        this.SetContacts(contacts);
        this.SetContactsPhone(contactsPhone);
        this.SetMemo(memo);
    }
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 实体版本
    /// </summary>
    public int EntityVersion { get; protected set; }

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
    public string? Contacts { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    public string? ContactsPhone { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Memo { get; set; }




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
    public List<ArrivedOrderItem> Items { get; set; } = new List<ArrivedOrderItem>();



    public virtual void AddItem(Guid id, Guid cargoId, string name, string bn, string sn, string? specDesc, int number, decimal? costPrice = null)
    {
        this.Items.Add(new ArrivedOrderItem(id, cargoId, name, bn, sn, specDesc, number, costPrice, this.TenantId));
    }


    public virtual void SetOrderNo(string orderNo)
    {
        this.OrderNo = Check.NotNullOrWhiteSpace(orderNo, nameof(orderNo), ArrivedOrderConsts.MaxOrderNoLength);
    }

    public virtual void SetBatchNo(string batchNo)
    {
        this.BatchNo = Check.NotNullOrWhiteSpace(batchNo, nameof(batchNo), ArrivedOrderConsts.MaxBatchNoLength);
    }
    public virtual void SetContacts(string? contacts)
    {
        this.Contacts = contacts;
    }
    public virtual void SetContactsPhone(string? contactsPhone)
    {
        this.ContactsPhone = contactsPhone;
    }
    public virtual void SetMemo(string? memo)
    {
        this.Memo = memo;
    }
    public virtual void SetExpectArrivedDate(DateOnly? expectArrivedDate)
    {
        this.ExpectArrivedDate = expectArrivedDate;
    }


    public virtual bool IsRemove()
    {
        return this.Status == ArrivedStatus.PreArrived;
    }



    public virtual void Arrived(DateTime arrivedDate)
    {
        if (!this.IsArrived()) return;

        this.Status = ArrivedStatus.ToBeUnload;
        this.ArrivedDate = arrivedDate;
    }
    public virtual bool IsArrived()
    {
        return this.Status == ArrivedStatus.PreArrived;
    }


    public virtual void Unload(DateTime unloadTime, string unloadOperator)
    {
        if (!this.IsUnload()) return;

        this.Status = ArrivedStatus.UnPick;
        this.UnloadTime = unloadTime;
        this.UnloadOperator = unloadOperator;

    }


    public virtual bool IsUnload()
    {
        return this.Status == ArrivedStatus.ToBeUnload;
    }

    public virtual void AddPick(ArrivedOrderItem item, int number, List<string>? seriesNumbers, Func<Guid> guidGenerator)
    {
        if (!this.CheckPickSeriesNumbers(number, seriesNumbers)) return;

        var surplusNumber = number;
        seriesNumbers = seriesNumbers ?? new List<string>();

        seriesNumbers.ForEach(a =>
        {
            surplusNumber--;
            item.AddPick(guidGenerator(), a, 1);
        });
        if (surplusNumber > 0)
            item.AddPick(guidGenerator(), null, surplusNumber);

    }

    public virtual bool CheckPickSeriesNumbers(int number, List<string>? seriesNumbers)
    {
        return number >= (seriesNumbers?.Count ?? 0);
    }

    public virtual bool IsPickItem(List<Guid> cargoIds)
    {
        return cargoIds.All(a => this.Items.Any(b => b.CargoId == a));
    }

    public virtual bool IsPick()
    {
        return this.Status == ArrivedStatus.UnPick;
    }

    public virtual void Pick()
    {
        if (!this.IsPick()) return;

        this.Status = ArrivedStatus.UnPutAway;

    }

    public virtual bool IsStock()
    {
        return this.Status == ArrivedStatus.UnPutAway;
    }

    public virtual void SetStorelocation(ArrivedOrderItem item, ArrivedOrderPickItem pickItem, Guid storelocationId, string storelocationCode)
    {
        if (!this.IsStock()) return;

        pickItem.SetStorelocation(storelocationId, storelocationCode);

        item.UpdateStockedNumber();
    }

    public virtual bool IsAllItemStock()
    {
        return this.Items.Any(b => !b.IsAllItemStock());
    }

    public virtual void Stock()
    {
        if (!this.IsStock()) return;
        if (!this.IsAllItemStock()) return;

        this.Status = ArrivedStatus.Completed;

    }

}

