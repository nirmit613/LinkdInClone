using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Linkd.Data;

/* This is used if database provider does't define
 * ILinkdDbSchemaMigrator implementation.
 */
public class NullLinkdDbSchemaMigrator : ILinkdDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
