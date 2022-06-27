using System;
using Glipotions.Blazor.Core.Services;
using Glipotions.OnMuhasebe.Parametreler;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class AppService : ICoreAppService, IScopedDependency
{
    public SelectFirmaParametreDto FirmaParametre { get; set; } = new SelectFirmaParametreDto
    {
        SubeId=new Guid("16480c84-0ceb-6412-dd63-3a03e4d6723f"),
        DonemId=new Guid("16480c84-0ceb-6412-dd63-3a03e4d6723f"),
    };    
    public Action HasChanged { get; set; }
    public bool ShowFirmaParametreEditPage { get; set; }
    public bool ShowSubeDonemEditPage { get; set; }
}