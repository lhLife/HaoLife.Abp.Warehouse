using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StorelocationDto : ExtensibleAuditedEntityDto<Guid>
{
    /// <summary>
    /// 库位编号
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 库区id
    /// </summary>
    public Guid StoreareaId { get; set; }
    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid StorehouseId { get; set; }

    /// <summary>
    /// 库区
    /// </summary>
    public StoreareaListDto Storearea { get; set; }

    /// <summary>
    /// 仓库
    /// </summary>
    public StorehouseListDto Storehouse { get; set; }

    /// <summary>
    /// 存储工具属性详细
    /// </summary>
    public string StoreToolAttrDesc { get; set; }
}
