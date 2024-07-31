using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(WarehouseHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class WarehouseConsoleApiClientModule : AbpModule
{

}
