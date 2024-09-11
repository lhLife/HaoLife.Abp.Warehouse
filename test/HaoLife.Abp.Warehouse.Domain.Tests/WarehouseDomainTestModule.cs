using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.MultiTenancy.ConfigurationStore;
using Volo.Abp.Settings;

namespace HaoLife.Abp.Warehouse;

[DependsOn(
    typeof(WarehouseDomainModule),
    typeof(WarehouseTestBaseModule)
)]
public class WarehouseDomainTestModule : AbpModule
{
}
