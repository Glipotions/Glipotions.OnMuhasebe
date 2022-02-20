using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Glipotions.OnMuhasebe.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class OnMuhasebeDbContextFactory : IDesignTimeDbContextFactory<OnMuhasebeDbContext>
{
    public OnMuhasebeDbContext CreateDbContext(string[] args)
    {
        OnMuhasebeEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<OnMuhasebeDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new OnMuhasebeDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Glipotions.OnMuhasebe.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
