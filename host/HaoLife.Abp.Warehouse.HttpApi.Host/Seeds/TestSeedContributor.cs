using DeviceDetectorNET;
using HaoLife.Abp.Warehouse.Cargos;
using HaoLife.Abp.Warehouse.GlobalFeatures;
using HaoLife.Abp.Warehouse.Storehouses;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Guids;

namespace HaoLife.Abp.Warehouse.Seeds;

public class TestSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IAbpLazyServiceProvider lazyServiceProvider;
    private readonly IGuidGenerator guidGenerator;

    public TestSeedContributor(
        IAbpLazyServiceProvider lazyServiceProvider
        , IGuidGenerator guidGenerator)
    {
        this.lazyServiceProvider = lazyServiceProvider;
        this.guidGenerator = guidGenerator;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        //var count = await this.cargoCategoryRepository.GetCountAsync();
        //if (count <= 0)
        //{
        //    var c = new CargoCategory(this.guidGenerator.Create(), "1", null, context.TenantId);
        //    var c1 = new CargoCategory(this.guidGenerator.Create(), "11", c.Id, context.TenantId);
        //    var c2 = new CargoCategory(this.guidGenerator.Create(), "12", c.Id, context.TenantId);
        //    var c3 = new CargoCategory(this.guidGenerator.Create(), "13", c.Id, context.TenantId);

        //    await this.cargoCategoryRepository.InsertManyAsync(new[] { c, c1, c2, c3 });
        //}
        if (GlobalFeatureManager.Instance.IsEnabled<StorehouseFeature>())
        {
            var storehouses = new Storehouse[] {
                new Storehouse(new System.Guid("548b9150-3bd7-db99-31ff-3a14a0013ef5"),"仓库1","350103","福州","台江区111","w4@live.cn","15xx555x","耗",true,context.TenantId)
            };

            var storeareas = new Storearea[] {
                new Storearea(new System.Guid("16338a85-bcad-4e82-dd7a-3a14a042c777"),"库区2",0,true,storehouses[0].Id,context.TenantId)
            };
            var storelocation = new Storelocation[] {
                new Storelocation(new System.Guid("ecc41915-fb01-e85a-cbcd-3a14a002efa6"),"k010102",true,storeareas[0].StorehouseId,storeareas[0].Id,context.TenantId)
            };


            foreach (var item in storehouses)
            {
                if (!await this.lazyServiceProvider.LazyGetRequiredService<IStorehouseRepository>().AnyAsync(a => a.Id == item.Id))
                {
                    await this.lazyServiceProvider.LazyGetRequiredService<IStorehouseRepository>().InsertAsync(item, true);

                }
            }

            foreach (var item in storeareas)
            {
                if (!await this.lazyServiceProvider.LazyGetRequiredService<IStoreareaRepository>().AnyAsync(a => a.Id == item.Id))
                {
                    await this.lazyServiceProvider.LazyGetRequiredService<IStoreareaRepository>().InsertAsync(item, true);

                }
            }

            foreach (var item in storelocation)
            {
                if (!await this.lazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().AnyAsync(a => a.Id == item.Id))
                {
                    await this.lazyServiceProvider.LazyGetRequiredService<IStorelocationRepository>().InsertAsync(item, true);

                }
            }
        }

        var cargos = new Cargo[] {
            new Cargo(new System.Guid("6fc7b5dc-3bb1-1384-9888-3a14aa6621c0"),"鞋子","https://img10.360buyimg.com/n1/jfs/t1/244542/2/5227/69539/65e2fcddF5641f596/bc6b6cc79c6b262c.jpg","a123456","a123456",true,123,"g","颜色:红",10,context.TenantId)
        };

        foreach (var item in cargos)
        {
            if (!await this.lazyServiceProvider.LazyGetRequiredService<ICargoRepository>().AnyAsync(a => a.Id == item.Id))
            {
                await this.lazyServiceProvider.LazyGetRequiredService<ICargoRepository>().InsertAsync(item, true);

            }
        }
    }
}
