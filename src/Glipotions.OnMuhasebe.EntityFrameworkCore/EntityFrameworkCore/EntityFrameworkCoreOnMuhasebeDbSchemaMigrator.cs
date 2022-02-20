using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Glipotions.OnMuhasebe.Data;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.EntityFrameworkCore;

public class EntityFrameworkCoreOnMuhasebeDbSchemaMigrator
    : IOnMuhasebeDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreOnMuhasebeDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the OnMuhasebeDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<OnMuhasebeDbContext>()
            .Database
            .MigrateAsync();
    }
}
