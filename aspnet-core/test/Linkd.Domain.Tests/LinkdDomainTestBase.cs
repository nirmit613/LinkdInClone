using Volo.Abp.Modularity;

namespace Linkd;

/* Inherit from this class for your domain layer tests. */
public abstract class LinkdDomainTestBase<TStartupModule> : LinkdTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
