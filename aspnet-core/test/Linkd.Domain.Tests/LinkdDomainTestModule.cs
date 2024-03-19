using Volo.Abp.Modularity;

namespace Linkd;

[DependsOn(
    typeof(LinkdDomainModule),
    typeof(LinkdTestBaseModule)
)]
public class LinkdDomainTestModule : AbpModule
{

}
