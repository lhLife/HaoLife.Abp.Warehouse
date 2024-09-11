using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Arriveds;

/// <summary>
/// 到货服务
/// </summary>
public interface IArrivedOrderAppService : IApplicationService
    //, ICrudAppService<ArrivedOrderDto, ArrivedOrderListDto, Guid, ArrivedOrderSearchDto, ArrivedOrderCreateDto, ArrivedOrderCreateDto>
    , ICreateAppService<ArrivedOrderDto, ArrivedOrderCreateDto>
    , IReadOnlyAppService<ArrivedOrderDto, ArrivedOrderListDto, Guid, ArrivedOrderSearchDto>
    , IDeleteAppService<Guid>
{

    /// <summary>
    /// 到货
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ArrivedOrderDto> ArrivedAsync(Guid id, ArrivedOrderArrivedDto input);


    /// <summary>
    /// 卸货
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ArrivedOrderDto> UnloadAsync(Guid id, ArrivedOrderUnloadDto input);

    /// <summary>
    /// 添加分拣项
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<ArrivedOrderDto> AddPickItemAsync(Guid id, List<ArrivedOrderPickDto> input);



    /// <summary>
    /// 分拣
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ArrivedOrderDto> PickAsync(Guid id);


    /// <summary>
    /// 上架
    /// </summary>
    /// <param name="id"></param>
    /// <param name="stocks"></param>
    /// <returns></returns>
    Task<ArrivedOrderDto> StockAsync(Guid id, List<ArrivedOrderStockDto> stocks);
}
