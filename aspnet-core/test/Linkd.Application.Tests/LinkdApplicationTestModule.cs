using Volo.Abp.Modularity;

namespace Linkd;

[DependsOn(
    typeof(LinkdApplicationModule),
    typeof(LinkdDomainTestModule)
)]
public class LinkdApplicationTestModule : AbpModule
{

}
