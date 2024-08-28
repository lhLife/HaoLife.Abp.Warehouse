using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Storehouses;

/// <summary>
/// 仓库
/// </summary>
public class Storehouse : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected Storehouse()
    {

    }
    public Storehouse(Guid id, string name, string adcode
        , string city, string fullAddress, string email, string phone, string liaisons
        , bool isEnable
        , Guid? tenantId = null)
        : base(id)
    {
        this.TenantId = tenantId;
        this.SetName(name);
        this.SetAdcode(adcode);
        this.SetCity(city);
        this.SetFullAddress(fullAddress);
        this.SetEmail(email);
        this.SetPhone(phone);
        this.SetLiaisons(liaisons);
        this.SetEnable(isEnable);

    }
    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// 仓库名称
    /// </summary>
    public string Name { get; protected set; }

    /// <summary>
    /// 城市行政编码
    /// </summary>
    public string Adcode { get; protected set; }

    /// <summary>
    /// 城市名称
    /// </summary>
    public string City { get; protected set; }

    /// <summary>
    /// 详细地址
    /// </summary>
    public string FullAddress { get; protected set; }

    /// <summary>
    /// 邮件
    /// </summary>
    public string Email { get; protected set; }
    /// <summary>
    /// 联系电话
    /// </summary>
    public string Phone { get; protected set; }

    /// <summary>
    /// 联系人
    /// </summary>
    public string Liaisons { get; protected set; }


    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; protected set; }


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), StorehouseConsts.MaxNameLength);
    }

    public virtual void SetAdcode(string adcode)
    {
        this.Adcode = Check.NotNullOrWhiteSpace(adcode, nameof(adcode), StorehouseConsts.MaxAdcodeLength);
    }
    public virtual void SetCity(string city)
    {
        this.City = Check.NotNullOrWhiteSpace(city, nameof(city), StorehouseConsts.MaxCityLength);
    }
    public virtual void SetFullAddress(string fullAddress)
    {
        this.FullAddress = Check.NotNullOrWhiteSpace(fullAddress, nameof(fullAddress), StorehouseConsts.MaxFullAddressLength);
    }
    public virtual void SetEmail(string email)
    {
        this.Email = Check.NotNullOrWhiteSpace(email, nameof(email), StorehouseConsts.MaxEmailLength);
    }
    public virtual void SetPhone(string phone)
    {
        this.Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone), StorehouseConsts.MaxPhoneLength);
    }
    public virtual void SetLiaisons(string liaisons)
    {
        this.Liaisons = Check.NotNullOrWhiteSpace(liaisons, nameof(liaisons), StorehouseConsts.MaxLiaisonsLength);
    }
    public virtual void SetEnable(bool enable)
    {
        this.IsEnable = enable;
    }
}
