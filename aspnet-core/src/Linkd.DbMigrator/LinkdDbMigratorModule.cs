using Linkd.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Linkd.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(LinkdEntityFrameworkCoreModule),
    typeof(LinkdApplicationContractsModule)
    )]
public class LinkdDbMigratorModule : AbpModule
{
}
