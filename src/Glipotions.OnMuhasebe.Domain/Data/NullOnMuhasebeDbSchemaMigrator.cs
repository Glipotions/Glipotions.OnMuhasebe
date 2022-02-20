using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Data;

/* This is used if database provider does't define
 * IOnMuhasebeDbSchemaMigrator implementation.
 */
public class NullOnMuhasebeDbSchemaMigrator : IOnMuhasebeDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
