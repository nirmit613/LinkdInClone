using Volo.Abp.Settings;

namespace Linkd.Settings;

public class LinkdSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(LinkdSettings.MySetting1));
    }
}
