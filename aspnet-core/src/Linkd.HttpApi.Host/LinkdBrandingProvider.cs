using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Linkd;

[Dependency(ReplaceServices = true)]
public class LinkdBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Linkd";
}
