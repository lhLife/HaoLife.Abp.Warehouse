using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace HaoLife.Abp.Warehouse.Storehouses;

public class StoreareaListDto : CreationAuditedEntityDto<Guid>
{
    /// <summary>
    /// 库区名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 库区类型
    /// </summary>
    public StoreareaType StoreareaType { get; set; }


    /// <summary>
    /// 是否可用
    /// </summary>
    public bool IsEnable { get; set; }

    /// <summary>
    /// 仓库id
    /// </summary>
    public Guid StorehouseId { get; set; }
}
