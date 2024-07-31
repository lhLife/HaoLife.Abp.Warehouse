using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
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
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
        });
    }
}
