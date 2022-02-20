using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Glipotions.OnMuhasebe.Blazor;

public class Program
{
    public async static Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
            .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

        try
        {
            Log.Information("Starting web host.");
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog();
            await builder.AddApplicationAsync<OnMuhasebeBlazorModule>();
            var app = builder.Build();
            await app.InitializeApplicationAsync();
            await app.RunAsync();
            return 0;
        }
        //catch (Exception ex)
        ////{
        ////    if (ex.GetType().Name != "StopTheHostException")
        ////        Log.Fatal(ex, "Host terminated unexpectedly!");

        ////    return 1;
        ////}
        //{
        //    //Log.Fatal(ex, "Host terminated unexpectedly!"); eski değeri kapattım
        //    //return 1; // eski değeri kapattım

        //    string type = ex.GetType().Name;
        //    if (type.Equals("StopTheHostException", StringComparison.Ordinal))
        //        throw;
        //    Log.Fatal(ex, "Host terminated unexpectedly!");
        //    return 1;
        //}
        catch (Exception ex)
        {
            string type = ex.GetType().Name;
            if (type.Equals("StopTheHostException", StringComparison.Ordinal))
            {
                throw;
            }

            Log.Fatal(ex, "Host terminated unexpectedly!");
            return 1;
        }

        finally
        {
            Log.CloseAndFlush();
        }
    }
}
