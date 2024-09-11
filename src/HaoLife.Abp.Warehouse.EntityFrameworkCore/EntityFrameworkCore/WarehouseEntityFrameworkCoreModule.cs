using HaoLife.Abp.Warehouse.Arriveds;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Dispatchs;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Stocks;
using HaoLife.Abp.Warehouse.Storehouses;
using HaoLife.Abp.Warehouse.Suppliers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

[DependsOn(
    typeof(WarehouseDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class WarehouseEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<WarehouseDbContext>(options =>
        {
            options.AddRepository<Cargo, EfCoreCargoRepository>();
            options.AddRepository<Stock, EfCoreStockRepository>();
            options.AddRepository<StockOptLog, EfCoreStockOptLogRepository>();

            //功能限制配置
            if (GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>())
                options.AddRepository<CargoCategory, EfCoreCargoCategoryRepository>();

            if (GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>())
                options.AddRepository<Supplier, EfCoreSupplierRepository>();

            if (GlobalFeatureManager.Instance.IsEnabled<CargoTypeSpecFeature>())
                options.AddRepository<CargoTypeSpec, EfCoreCargoTypeSpecRepository>();

            if (GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>())
                options.AddRepository<StoreTool, EfCoreStoreToolRepository>();

            if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
            {
                options.AddRepository<Storehouse, EfCoreStorehouseRepository>();
                options.AddRepository<Storearea, EfCoreStoreareaRepository>();
                options.AddRepository<Storelocation, EfCoreStorelocationRepository>();
            }

            if (GlobalFeatureManager.Instance.IsEnabled<ArrivedOrderFreature>())
                options.AddRepository<ArrivedOrder, EfCoreArrivedOrderRepository>();

            if (GlobalFeatureManager.Instance.IsEnabled<DispatchOrderFeature>())
                options.AddRepository<DispatchOrder, EfCoreDispatchOrderRepository>();



            if (GlobalFeatureManager.Instance.IsEnabled<ArrivedOrderFreature>())
                options.Entity<ArrivedOrder>(entity => entity.DefaultWithDetailsFunc = 
                    (a) => a.Include(b => b.Items).ThenInclude(a => a.Picks));
        });

        //Configure<AbpEntityOptions>(options =>
        //{
        //    //配置Include
        //    //if (GlobalFeatureManager.Instance.IsEnabled<CargoTypeSpecFeature>())
        //    //    options.Entity<CargoTypeSpec>(entity => entity.DefaultWithDetailsFunc = (a) => a.Include(b => b.Values));

        //    //if (GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>())
        //    //    options.Entity<StoreTool>(entity => entity.DefaultWithDetailsFunc = (a) => a.Include(b => b.Attrs));

        //    //if (GlobalFeatureManager.Instance.IsEnabled<ArrivedOrderFreature>())
        //    //    options.Entity<ArrivedOrder>(entity => entity.DefaultWithDetailsFunc =
        //    //        (a) => a.Include(b => b.Itmes).ThenInclude(c => c.Sorts));

        //    //if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        //    //{
        //    //    //options.Entity<Storearea>(entity => entity.DefaultWithDetailsFunc = (a) => a.Include(b => b.Storehouse));
        //    //    //options.Entity<Storelocation>(entity => entity.DefaultWithDetailsFunc =
        //    //    //    (a) => a.Include(b => b.Storehouse).Include(b => b.Storearea));
        //    //}

        //    if (GlobalFeatureManager.Instance.IsEnabled<ArrivedOrderFreature>())
        //        options.Entity<ArrivedOrder>(entity => entity.DefaultWithDetailsFunc =
        //            (a) => a.Include(b => b.Items).ThenInclude(a => a.Picks));

        //});

    }
}
