using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace HaoLife.Abp.Warehouse.Stocks;

/// <summary>
/// 库存服务
/// </summary>
public interface IStockAppService : IApplicationService
{
    /// <summary>
    /// 添加库存
    /// </summary>
    /// <param name="input">库存操作入参</param>
    /// <returns></returns>
    Task<StockDto> AddAsync(StockInvokeDto input);

    /// <summary>
    /// 批量添加库存
    /// </summary>
    /// <param name="input">批量库存操作</param>
    /// <returns></returns>
    Task<List<StockDto>> AddBatchAsync(List<StockInvokeDto> input);

    /// <summary>
    /// 扣除库存
    /// </summary>
    /// <param name="input">库存操作入参</param>
    /// <returns></returns>
    Task<StockDto> DeductAsync(StockInvokeDto input);

    /// <summary>
    /// 批量扣除库存
    /// </summary>
    /// <param name="input">批量库存操作</param>
    /// <returns></returns>
    Task<List<StockDto>> DeductBatchAsync(List<StockInvokeDto> input);


    /// <summary>
    /// 冻结库存
    /// </summary>
    /// <param name="id">库存id</param>
    /// <returns></returns>
    Task<StockDto> FreezeAsync(Guid id);

    /// <summary>
    /// 批量冻结库存
    /// </summary>
    /// <param name="ids">批量库存id</param>
    /// <returns></returns>
    Task<List<StockDto>> FreezeBatchAsync(List<Guid> ids);

    /// <summary>
    /// 解冻库存
    /// </summary>
    /// <param name="id">库存id</param>
    /// <returns></returns>
    Task<StockDto> UnFreezeAsync(Guid id);

    /// <summary>
    /// 批量解冻库存
    /// </summary>
    /// <param name="ids">批量库存id</param>
    /// <returns></returns>
    Task<List<StockDto>> UnFreezeBatchAsync(List<Guid> ids);


    /// <summary>
    /// 库存查询
    /// </summary>
    /// <param name="input">库存查询入参</param>
    /// <returns></returns>
    Task<PagedResultDto<StockDto>> GetStackPagedListAsync(StockSearchDto input);


    /// <summary>
    /// 货物库存查询
    /// </summary>
    /// <param name="input">货物库存查询入参</param>
    /// <returns></returns>
    Task<PagedResultDto<CargoStockDto>> GetCargoStockPagedListAsync(CargoStockSearchDto input);
}
