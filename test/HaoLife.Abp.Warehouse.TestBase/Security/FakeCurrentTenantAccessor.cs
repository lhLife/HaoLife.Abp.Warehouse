using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;

namespace HaoLife.Abp.Warehouse.Security;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(ICurrentTenantAccessor), typeof(FakeCurrentTenantAccessor))]
public class FakeCurrentTenantAccessor : ICurrentTenantAccessor, ISingletonDependency
{
    public BasicTenantInfo? Current { get; set; } = new BasicTenantInfo(new Guid("10000000-0000-0000-0000-000000000001"), "测试");
}
