using HaoLife.Abp.Warehouse.Arriveds;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.Dispatchs;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.StockFreezes;
using HaoLife.Abp.Warehouse.StockProcesses;
using HaoLife.Abp.Warehouse.Stocks;
using HaoLife.Abp.Warehouse.Storehouses;
using HaoLife.Abp.Warehouse.Suppliers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.EntityFrameworkCore.ValueConverters;
using Volo.Abp.GlobalFeatures;

namespace HaoLife.Abp.Warehouse.EntityFrameworkCore;

public static class WarehouseDbContextModelCreatingExtensions
{
    public static void ConfigureWarehouse(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Cargo>(b =>
        {
            //Configure table & schema name
            b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Cargo)}s", WarehouseDbProperties.DbSchema
                , b => b.HasComment("货物"));

            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();

            //Properties
            b.HasKey(q => q.Id);
            b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
            b.Property(q => q.Name).IsRequired().HasMaxLength(CargoConsts.MaxNameLength).HasComment("货物名称");
            b.Property(q => q.Images).IsRequired().HasMaxLength(CargoConsts.MaxImagesLength).HasComment("货物图片");
            b.Property(q => q.Bn).IsRequired().HasMaxLength(CargoConsts.MaxCodeLength).HasComment("货物条码");
            b.Property(q => q.Sn).IsRequired().HasMaxLength(CargoConsts.MaxCodeLength).HasComment("货物编码");
            b.Property(q => q.Weight).IsRequired(false).HasPrecision(CommonConsts.PrecisionLength, CommonConsts.ScaleLength).HasComment("重量");
            b.Property(q => q.Unit).IsRequired(false).HasMaxLength(CargoConsts.MaxUnitLength).HasComment("单位");
            b.Property(q => q.SpecDesc).IsRequired(false).HasMaxLength(CargoConsts.MaxSpecDescLength).HasComment("规格描述");
            b.Property(q => q.IsEnable).IsRequired(true).HasComment("是否可用");
            b.Property(q => q.CostPrice).IsRequired(false).HasPrecision(CommonConsts.PrecisionLength, CommonConsts.ScaleLength).HasComment("货物成本单价");

            b.Property(q => q.CategoryId).IsRequired(false).HasComment("货物类别id");
            b.Property(q => q.SupplierId).IsRequired(false).HasComment("供应商id");

            //Relations
            if (GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>())
                b.HasOne<CargoCategory>().WithMany().HasForeignKey(a => a.CategoryId);
            if (GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>())
                b.HasOne<Supplier>().WithMany().HasForeignKey(a => a.SupplierId);

        });

        builder.Entity<Stock>(b =>
        {
            //Configure table & schema name
            b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Stock)}s", WarehouseDbProperties.DbSchema
                , b => b.HasComment("库存"));

            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();

            //Properties
            b.HasKey(q => q.Id);
            b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
            b.Property(q => q.CargoSn).IsRequired().HasMaxLength(CargoConsts.MaxCodeLength).HasComment("货物编码");
            b.Property(q => q.Number).IsRequired().HasComment("库存数量");
            b.Property(q => q.SeriesNumber).IsRequired(false).HasMaxLength(StockConsts.MaxSeriesNumberLength).HasComment("库存序列号");
            b.Property(q => q.IsFreeze).IsRequired().HasComment("是否冻结");
            b.Property(q => q.BatchNo).IsRequired(false).HasMaxLength(StockConsts.MaxBatchNoLength).HasComment("批次号");

            b.Property(q => q.StorehouseId).IsRequired(false).HasComment("仓库id");
            b.Property(q => q.StoreareaId).IsRequired(false).HasComment("库区id");
            b.Property(q => q.StorelocationId).IsRequired(false).HasComment("库位id");
            b.Property(q => q.CargoId).IsRequired().HasComment("货物id");

            //Relations
            b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);

            if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
            {
                b.HasOne<Storehouse>().WithMany().HasForeignKey(a => a.StorehouseId);
                b.HasOne<Storearea>().WithMany().HasForeignKey(a => a.StoreareaId);
                b.HasOne<Storelocation>().WithMany().HasForeignKey(a => a.StorelocationId);
            }
        });

        builder.Entity<StockOptLog>(b =>
        {
            //Configure table & schema name
            b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StockOptLog)}s", WarehouseDbProperties.DbSchema
                , b => b.HasComment("库存操作日志"));

            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();

            //Properties
            b.HasKey(q => q.Id);
            b.Property(q => q.StockOptType).IsRequired().HasComment("库存操作类型");
            b.Property(q => q.OptNumber).IsRequired().HasComment("操作数量");
            b.Property(q => q.CurrentNumber).IsRequired().HasComment("当前数量");
            b.Property(q => q.ResultNumber).IsRequired().HasComment("结果数量");

            b.Property(q => q.StorelocationId).IsRequired(false).HasComment("库位id");
            b.Property(q => q.CargoId).IsRequired().HasComment("货物id");
            b.Property(q => q.StockId).IsRequired().HasComment("库存id");

            //Relations
            b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);
            b.HasOne<Stock>().WithMany().HasForeignKey(a => a.StockId);

            if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
                b.HasOne<Storelocation>().WithMany().HasForeignKey(a => a.StorelocationId);
        });

        if (GlobalFeatureManager.Instance.IsEnabled<CargoCategoryFeature>())
        {
            builder.Entity<CargoCategory>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(CargoCategory)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("货物类别"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(CargoCategoryConsts.MaxNameLength).HasComment("货物类别名称");
                b.Property(q => q.ParentId).IsRequired(false).HasComment("所属类别id");

                //Relations
                b.HasOne<CargoCategory>().WithMany().HasForeignKey(q => q.ParentId);

            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<CargoTypeSpecFeature>())
        {

            builder.Entity<CargoTypeSpec>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(CargoTypeSpec)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("货物类型规格"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(CargoTypeSpecConsts.MaxNameLength).HasComment("规格名称");
                b.Property(q => q.Sort).IsRequired().HasComment("排序");

                ////Relations
                //b.HasOne(p => p.Values).WithMany().HasForeignKey($"{nameof(CargoTypeSpec)}Id");

                b.Property(q => q.Values).IsRequired().HasComment("货物规格值")
                    .HasConversion(new AbpJsonValueConverter<List<CargoTypeSpecValue>>());

            });

            //builder.Entity<CargoTypeSpecValue>(b =>
            //{
            //    //Configure table & schema name
            //    b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(CargoTypeSpecValueConsts)}s", WarehouseDbProperties.DbSchema
            //        , b => b.HasComment("货物类型规格值"));

            //    b.ConfigureByConvention();
            //    b.ApplyObjectExtensionMappings();

            //    //Properties
            //    b.HasKey(q => q.Id);
            //    b.Property(q => q.Value).IsRequired().HasMaxLength(CargoTypeSpecConsts.MaxNameLength).HasComment("规格值");
            //    b.Property(q => q.Sort).IsRequired().HasComment("值排序");

            //});
        }

        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {

            builder.Entity<Storehouse>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Storehouse)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("仓库"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(StorehouseConsts.MaxNameLength).HasComment("仓库名称");
                b.Property(q => q.Adcode).IsRequired().HasMaxLength(StorehouseConsts.MaxAdcodeLength).HasComment("城市行政编码");
                b.Property(q => q.City).IsRequired().HasMaxLength(StorehouseConsts.MaxCityLength).HasComment("城市名称");
                b.Property(q => q.FullAddress).IsRequired().HasMaxLength(StorehouseConsts.MaxFullAddressLength).HasComment("详细地址");
                b.Property(q => q.Email).IsRequired(false).HasMaxLength(StorehouseConsts.MaxEmailLength).HasComment("邮件");
                b.Property(q => q.Phone).IsRequired(false).HasMaxLength(StorehouseConsts.MaxPhoneLength).HasComment("联系电话");
                b.Property(q => q.Liaisons).IsRequired(false).HasMaxLength(StorehouseConsts.MaxLiaisonsLength).HasComment("联系人");
                b.Property(q => q.IsEnable).IsRequired().HasComment("是否可用");

            });

            builder.Entity<Storearea>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Storearea)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("库区"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(StoreareaConsts.MaxNameLength).HasComment("库区名称");
                b.Property(q => q.StoreareaType).IsRequired().HasComment("库区类型");
                b.Property(q => q.IsEnable).IsRequired().HasComment("是否可用");

                b.Property(q => q.StorehouseId).IsRequired().HasComment("仓库id");

                //Relations
                b.HasOne<Storehouse>().WithMany().HasForeignKey(a => a.StorehouseId);

            });

            builder.Entity<Storelocation>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Storelocation)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("库位"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Code).IsRequired().HasMaxLength(StorelocationConsts.MaxCodeLength).HasComment("库位编号");
                b.Property(q => q.IsEnable).IsRequired().HasComment("是否可用");
                b.Property(q => q.StoreToolAttrDesc).IsRequired(false).HasMaxLength(StorelocationConsts.MaxStoreToolSpecDescLength)
                    .HasComment("存储工具属性描述");

                b.Property(q => q.StorehouseId).IsRequired().HasComment("库位id");
                b.Property(q => q.StoreareaId).IsRequired().HasComment("库区id");

                //Relations
                b.HasOne<Storehouse>().WithMany().HasForeignKey(a => a.StorehouseId).IsRequired();
                b.HasOne<Storearea>().WithMany().HasForeignKey(a => a.StoreareaId).IsRequired();

            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<StoreToolFeature>())
        {

            builder.Entity<StoreTool>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StoreTool)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("存储工具"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(StoreToolConsts.MaxNameLength).HasComment("名称");
                b.Property(q => q.Sort).IsRequired().HasComment("排序");

                ////Relations
                //b.HasMany(a => a.Values).WithOne().HasForeignKey($"{nameof(StoreToolSpec)}Id").IsRequired();


                b.Property(q => q.Attrs).IsRequired().HasComment("存储工具属性")
                    .HasConversion(new AbpJsonValueConverter<List<StoreToolAttr>>());

            });

            //builder.Entity<StoreToolAttr>(b =>
            //{
            //    //Configure table & schema name
            //    b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StoreToolAttr)}s", WarehouseDbProperties.DbSchema
            //        , b => b.HasComment("存储工具属性"));

            //    b.TryConfigureMultiTenant();
            //    b.ApplyObjectExtensionMappings();

            //    //Properties
            //    b.HasKey(q => q.Id);
            //    b.Property(q => q.Name).IsRequired().HasMaxLength(StoreToolAttrConsts.MaxNameLength).HasComment("属性名称");
            //    b.Property(q => q.Sort).IsRequired().HasComment("排序");
            //});
        }

        if (GlobalFeatureManager.Instance.IsEnabled<SupplierFeature>())
        {
            builder.Entity<Supplier>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(Supplier)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("供应商"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(SupplierConsts.MaxNameLength).HasComment("仓库名称");
                b.Property(q => q.Adcode).IsRequired().HasMaxLength(SupplierConsts.MaxAdcodeLength).HasComment("城市行政编码");
                b.Property(q => q.City).IsRequired().HasMaxLength(SupplierConsts.MaxCityLength).HasComment("城市名称");
                b.Property(q => q.FullAddress).IsRequired().HasMaxLength(SupplierConsts.MaxFullAddressLength).HasComment("详细地址");
                b.Property(q => q.Email).IsRequired(false).HasMaxLength(SupplierConsts.MaxEmailLength).HasComment("邮件");
                b.Property(q => q.Phone).IsRequired(false).HasMaxLength(SupplierConsts.MaxPhoneLength).HasComment("联系电话");
                b.Property(q => q.Liaisons).IsRequired(false).HasMaxLength(SupplierConsts.MaxLiaisonsLength).HasComment("联系人");
            });

        }

        if (GlobalFeatureManager.Instance.IsEnabled<ArrivedOrderFreature>())
        {

            builder.Entity<ArrivedOrder>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(ArrivedOrder)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("到货单"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
                b.Property(q => q.ExpectArrivedDate).IsRequired(false).HasComment("预计到达时间");
                b.Property(q => q.BatchNo).IsRequired().HasComment("批次号");
                b.Property(q => q.Contacts).IsRequired(false).HasMaxLength(ArrivedOrderConsts.MaxContactsLength).HasComment("联系人");
                b.Property(q => q.ContactsPhone).IsRequired().HasMaxLength(ArrivedOrderConsts.MaxContactsPhoneLength).HasComment("联系人电话");
                b.Property(q => q.Memo).IsRequired(false).HasMaxLength(ArrivedOrderConsts.MaxMemoLength).HasComment("备注");
                b.Property(q => q.ArrivedDate).IsRequired(false).HasComment("到达时间");
                b.Property(q => q.UnloadTime).IsRequired(false).HasComment("卸货时间");
                b.Property(q => q.UnloadOperator).HasMaxLength(ArrivedOrderConsts.MaxUnloadOperatorLength).IsRequired(false).HasComment("卸货操作人");
                b.Property(q => q.Status).IsRequired().HasComment("到货状态");


                b.HasMany(a => a.Itmes).WithOne().HasForeignKey($"{nameof(ArrivedOrder)}Id");

            });

            builder.Entity<ArrivedOrderItem>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(ArrivedOrderItem)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("到货单货物明细"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Name).IsRequired().HasMaxLength(CargoConsts.MaxNameLength).HasComment("货物名称");
                b.Property(q => q.Bn).IsRequired().HasMaxLength(CargoConsts.MaxCodeLength).HasComment("货物条码");
                b.Property(q => q.Sn).IsRequired().HasMaxLength(CargoConsts.MaxCodeLength).HasComment("货物编码");
                b.Property(q => q.SpecDesc).IsRequired(false).HasMaxLength(CargoConsts.MaxSpecDescLength).HasComment("规格描述");
                b.Property(q => q.Number).IsRequired().HasComment("到货数量");
                b.Property(q => q.CostPrice).IsRequired(false).HasComment("货物成本单价");
                b.Property(q => q.SortNumber).IsRequired().HasComment("分拣数量");
                b.Property(q => q.StockedNumber).IsRequired().HasComment("已入库数");

                b.Property(q => q.CargoId).IsRequired().HasComment("货物id");

                //Relations
                b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);
                b.HasMany(a => a.Sorts).WithOne().HasForeignKey($"{nameof(ArrivedOrderItem)}Id");

            });

            builder.Entity<ArrivedOrderSortItem>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(ArrivedOrderSortItem)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("到货单货物分拣明细"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.SeriesNumber).IsRequired(false).HasMaxLength(StockConsts.MaxSeriesNumberLength).HasComment("库存序列号");
                b.Property(q => q.Seq).IsRequired().HasComment("序号");
                b.Property(q => q.Number).IsRequired().HasComment("分拣数量");
                b.Property(q => q.StockNumber).IsRequired().HasComment("已入库数");
                b.Property(q => q.IsStock).IsRequired().HasComment("是否同步库存");

            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<DispatchOrderFeature>())
        {
            builder.Entity<DispatchOrder>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(DispatchOrder)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("发货单"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
                b.Property(q => q.DispatchOrderNo).IsRequired().HasComment("发货单号");
                b.Property(q => q.TotalNumber).IsRequired().HasComment("总数量");
                b.Property(q => q.TotalWeight).IsRequired().HasPrecision(CommonConsts.PrecisionLength, CommonConsts.ScaleLength).HasComment("总重量");
                b.Property(q => q.Stauts).IsRequired().HasComment("发货状态");
                b.Property(q => q.PackOperater).IsRequired(false).HasMaxLength(DispatchOrderConsts.MaxPackOperaterLength).HasComment("打包人");
                b.Property(q => q.PackTime).IsRequired(false).HasComment("打包时间");
                b.Property(q => q.MeteringOperater).IsRequired(false).HasMaxLength(DispatchOrderConsts.MaxMeteringOperaterLength).HasComment("称重人");
                b.Property(q => q.MeteringTime).IsRequired(false).HasComment("称重时间");

            });

            builder.Entity<DispatchOrderItem>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(DispatchOrderItem)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("发货单明细"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Number).IsRequired().HasComment("发货数量");
                b.Property(q => q.LockNumber).IsRequired().HasComment("锁定数量");
                b.Property(q => q.PickedNumber).IsRequired().HasComment("已选数量");

                b.Property(q => q.CargoId).IsRequired().HasComment("货物id");

                //Relations
                b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);

            });

            builder.Entity<DispatchOrderPickItem>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(DispatchOrderPickItem)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("发货单拣货明细"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.PickNumber).IsRequired().HasComment("挑选数量");
                b.Property(q => q.SeriesNumber).IsRequired(false).HasMaxLength(StockConsts.MaxSeriesNumberLength).HasComment("库存序列号");
                b.Property(q => q.PickedNumber).IsRequired().HasComment("已选数量");
                b.Property(q => q.IsStock).IsRequired().HasComment("是否同步库存");

                b.Property(q => q.StorelocationId).IsRequired().HasComment("库位id");

                //Relations
                b.HasOne<Storelocation>().WithMany().HasForeignKey(a => a.StorelocationId);

            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<StockFreezeFeature>())
        {

            builder.Entity<StockFreeze>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StockFreeze)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("库存冻结"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
                b.Property(q => q.JobCode).IsRequired().HasMaxLength(StockFreezeConsts.MaxJobCodeLength).HasComment("作业编号");
                b.Property(q => q.SeriesNumber).IsRequired(false).HasMaxLength(StockConsts.MaxSeriesNumberLength).HasComment("库存序列号");
                b.Property(q => q.Operater).IsRequired(false).HasMaxLength(StockFreezeConsts.MaxOperaterLength).HasComment("操作人");
                b.Property(q => q.OperateTime).IsRequired(false).HasComment("操作时间");

                b.Property(q => q.StorelocationId).IsRequired().HasComment("库位id");
                b.Property(q => q.CargoId).IsRequired().HasComment("货物id");


                //Relations
                b.HasOne<Storelocation>().WithMany().HasForeignKey(a => a.StorelocationId);
                b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);

            });
        }

        if (GlobalFeatureManager.Instance.IsEnabled<StockProcessFeature>())
        {

            builder.Entity<StockProcess>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StockProcess)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("库存加工"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.EntityVersion).IsRequired().HasComment("实体版本");
                b.Property(q => q.JobCode).IsRequired().HasMaxLength(StockProcessConsts.MaxJobCodeLength).HasComment("作业编号");
                b.Property(q => q.Type).IsRequired().HasComment("库存加工类型");
                b.Property(q => q.Status).IsRequired().HasComment("库存加工状态");
                b.Property(q => q.Operater).IsRequired(false).HasMaxLength(StockFreezeConsts.MaxOperaterLength).HasComment("操作人");
                b.Property(q => q.OperateTime).IsRequired(false).HasComment("操作时间");

                //Relations
                b.HasMany(a => a.Items).WithOne().HasForeignKey($"{nameof(StockProcess)}Id");

            });

            builder.Entity<StockProcessItem>(b =>
            {
                //Configure table & schema name
                b.ToTable(WarehouseDbProperties.DbTablePrefix + $"{nameof(StockProcessItem)}s", WarehouseDbProperties.DbSchema
                    , b => b.HasComment("库存加工项"));

                b.ConfigureByConvention();
                b.ApplyObjectExtensionMappings();

                //Properties
                b.HasKey(q => q.Id);
                b.Property(q => q.Number).IsRequired().HasComment("操作数量");
                b.Property(q => q.IsSource).HasComment("是否来源");
                b.Property(q => q.SeriesNumber).IsRequired(false).HasMaxLength(StockConsts.MaxSeriesNumberLength).HasComment("库存序列号");

                b.Property(q => q.CargoId).IsRequired().HasComment("货物id");
                b.Property(q => q.StorelocationId).IsRequired().HasComment("库位id");

                //Relations
                b.HasOne<Storelocation>().WithMany().HasForeignKey(a => a.StorelocationId);
                b.HasOne<Cargo>().WithMany().HasForeignKey(a => a.CargoId);

            });
        }

    }
}
