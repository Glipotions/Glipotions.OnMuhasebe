using System;
using System.IO;
using System.Net.Http;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Glipotions.OnMuhasebe.Blazor.Menus;
using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Glipotions.OnMuhasebe.Localization;
using Glipotions.OnMuhasebe.MultiTenancy;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic.Bundling;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.AutoMapper;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.UI;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using System.Text.Json.Serialization;
using DevExpress.Blazor.Configuration;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using DevExpress.Blazor.Reporting.Controllers;

namespace Glipotions.OnMuhasebe.Blazor;

[DependsOn(
    typeof(OnMuhasebeApplicationModule),
    typeof(OnMuhasebeEntityFrameworkCoreModule),
    typeof(OnMuhasebeHttpApiModule),
    typeof(AbpAspNetCoreMvcUiBasicThemeModule),
    typeof(AbpAutofacModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAccountWebIdentityServerModule),
    typeof(AbpAspNetCoreComponentsServerBasicThemeModule),
    typeof(AbpIdentityBlazorServerModule),
    typeof(AbpTenantManagementBlazorServerModule),
    typeof(AbpSettingManagementBlazorServerModule)
   )]
public class OnMuhasebeBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(OnMuhasebeResource),
                typeof(OnMuhasebeDomainModule).Assembly,
                typeof(OnMuhasebeDomainSharedModule).Assembly,
                typeof(OnMuhasebeApplicationModule).Assembly,
                typeof(OnMuhasebeApplicationContractsModule).Assembly,
                typeof(OnMuhasebeBlazorModule).Assembly
            );
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        ConfigureUrls(configuration);
        ConfigureBundles();
        ConfigureAuthentication(context, configuration);
        ConfigureAutoMapper();
        ConfigureVirtualFileSystem(hostingEnvironment);
        ConfigureLocalizationServices();
        ConfigureSwaggerServices(context.Services);
        ConfigureAutoApiControllers();
        ConfigureBlazorise(context);
        ConfigureRouter(context);
        ConfigureMenu(context);

        // Bizim oluşturduğumuz Configureler
        //ConfigureJson(context);
        ConfigureDevExpress(context);
        ConfigureDevExpressReport(context);
    }
    /// <Özet>
    /// (2/5) 14. Video 55. Dk dan itibaren
    /// DEVRE DIŞI BIRAKTIK
    /// <param name="context"></param>
    //private void ConfigureJson(ServiceConfigurationContext context)
    //{
    //    context.Services.AddControllers()
    //        .AddJsonOptions(x => x.JsonSerializerOptions
    //        .ReferenceHandler = ReferenceHandler.IgnoreCycles);
    //}

    /// ÖZET
    /// (5/5) 18. video 43. dk
    /// 
    private void ConfigureAntiForgery()
    {
        Configure<AbpAntiForgeryOptions>(options =>
        {
            options.AutoValidateFilter = type => type == typeof(DownloadExportResultControllerBase);
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }

    private void ConfigureBundles()
    {
        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                BasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/global-styles.css");
                }
            );
              
            //BLAZOR UI
            options.StyleBundles.Configure(
                BlazorBasicThemeBundles.Styles.Global,
                bundle =>
                {
                    bundle.AddFiles("/css/blazing_berry/bootstrap.min.css");
                    bundle.AddFiles("/css/site.css");
                    bundle.AddFiles("/blazor-global-styles.css");
                    bundle.AddFiles("/Glipotions.OnMuhasebe.Blazor.styles.css");
                    bundle.AddFiles("/_content/DevExpress.Blazor/dx-blazor.bs5.css");
                    bundle.AddFiles("/_content/DevExpress.Blazor.Reporting.Viewer/css/dx-blazor-reporting-components.css");
                    bundle.AddFiles("/_content/Glipotions.Blazor.Core/css/component.css");
                }
            );

            options.ScriptBundles.Configure(
                BlazorBasicThemeBundles.Scripts.Global,
                bundle=>
                {
                    //Varsa js dosya yolları eklenecek.
                    //bundle.AddFiles()
                }
            );
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication()
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = "OnMuhasebe";
            });
    }

    private void ConfigureVirtualFileSystem(IWebHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<OnMuhasebeDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Glipotions.OnMuhasebe.Domain.Shared"));
                options.FileSets.ReplaceEmbeddedByPhysical<OnMuhasebeDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Glipotions.OnMuhasebe.Domain"));
                options.FileSets.ReplaceEmbeddedByPhysical<OnMuhasebeApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Glipotions.OnMuhasebe.Application.Contracts"));
                options.FileSets.ReplaceEmbeddedByPhysical<OnMuhasebeApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Glipotions.OnMuhasebe.Application"));
                options.FileSets.ReplaceEmbeddedByPhysical<OnMuhasebeBlazorModule>(hostingEnvironment.ContentRootPath);
            });
        }
    }

    private void ConfigureLocalizationServices()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("is", "is", "Icelandic", "is"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ro-RO", "ro-RO", "Română"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }

    private void ConfigureSwaggerServices(IServiceCollection services)
    {
        services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "OnMuhasebe API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            }
        );
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new OnMuhasebeMenuContributor());
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(OnMuhasebeBlazorModule).Assembly;
        });
    }

    private void ConfigureAutoApiControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(OnMuhasebeApplicationModule).Assembly);
        });
    }

    private void ConfigureAutoMapper()
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<OnMuhasebeBlazorModule>();
        });
    }
    private void ConfigureDevExpress(ServiceConfigurationContext context)
    {
        context.Services.AddDevExpressBlazor();
        context.Services.Configure<GlobalOptions>(options =>
        {
            options.BootstrapVersion = DevExpress.Blazor.BootstrapVersion.v5;
        });
    }

    private void ConfigureDevExpressReport(ServiceConfigurationContext context)
    {
        context.Services.AddDevExpressServerSideBlazorReportViewer();
    }
    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseDevExpressServerSideBlazorReportViewer();
        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseJwtTokenMiddleware();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "OnMuhasebe API");
        });
        app.UseConfiguredEndpoints();
    }
}
