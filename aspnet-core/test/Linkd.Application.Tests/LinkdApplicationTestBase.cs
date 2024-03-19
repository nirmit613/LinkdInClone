using Volo.Abp.Modularity;

namespace Linkd;

public abstract class LinkdApplicationTestBase<TStartupModule> : LinkdTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
