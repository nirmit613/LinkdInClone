using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Linkd.Data;
using Volo.Abp.DependencyInjection;

namespace Linkd.EntityFrameworkCore;

public class EntityFrameworkCoreLinkdDbSchemaMigrator
    : ILinkdDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreLinkdDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolve the LinkdDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<LinkdDbContext>()
            .Database
            .MigrateAsync();
    }
}
