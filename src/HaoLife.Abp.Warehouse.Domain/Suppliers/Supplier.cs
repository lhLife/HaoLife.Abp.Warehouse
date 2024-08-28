using HaoLife.Abp.Warehouse.Cargos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Suppliers;

/// <summary>
/// 供应商
/// </summary>
public class Supplier : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    protected Supplier()
    {
        
    }
    public Supplier(Guid id, string name, string adcode, string city, string fullAddress, string email, string phone, string liaisons
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
    }

    /// <summary>
    /// 租户id
    /// </summary>
    public virtual Guid? TenantId { get; protected set; }


    /// <summary>
    /// 供应商名称
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


    public virtual void SetName(string name)
    {
        this.Name = Check.NotNullOrWhiteSpace(name, nameof(name), SupplierConsts.MaxNameLength);
    }

    public virtual void SetAdcode(string adcode)
    {
        this.Adcode = Check.NotNullOrWhiteSpace(adcode, nameof(adcode), SupplierConsts.MaxAdcodeLength);
    }

    public virtual void SetCity(string city)
    {
        this.City = Check.NotNullOrWhiteSpace(city, nameof(city), SupplierConsts.MaxCityLength);
    }
    public virtual void SetFullAddress(string fullAddress)
    {
        this.FullAddress = Check.NotNullOrWhiteSpace(fullAddress, nameof(fullAddress), SupplierConsts.MaxFullAddressLength);
    }
    public virtual void SetEmail(string email)
    {
        this.Email = Check.NotNullOrWhiteSpace(email, nameof(email), SupplierConsts.MaxEmailLength);
    }
    public virtual void SetPhone(string phone)
    {
        this.Phone = Check.NotNullOrWhiteSpace(phone, nameof(phone), SupplierConsts.MaxPhoneLength);
    }
    public virtual void SetLiaisons(string liaisons)
    {
        this.Liaisons = Check.NotNullOrWhiteSpace(liaisons, nameof(liaisons), SupplierConsts.MaxLiaisonsLength);
    }
}
