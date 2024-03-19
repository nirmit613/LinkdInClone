using System.Threading.Tasks;

namespace Linkd.Data;

public interface ILinkdDbSchemaMigrator
{
    Task MigrateAsync();
}
