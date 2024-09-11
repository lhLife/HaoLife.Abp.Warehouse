using Volo.Abp.Settings;

namespace HaoLife.Abp.Warehouse.Settings;

public class WarehouseSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        /* Define module settings here.
         * Use names from WarehouseSettings class.
         */

        context.Add(new SettingDefinition(
            WarehouseSettings.ArrivedOrderNoGenerateTemplate, """A{{ date.now | date.to_string '%y%m%d%H%M%S' }}{{ r | string.slice 3}}"""));
    }
}
