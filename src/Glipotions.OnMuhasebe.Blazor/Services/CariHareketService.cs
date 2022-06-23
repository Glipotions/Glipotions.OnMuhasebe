using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Cariler;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class CariHareketService : BaseService<ListCariHareketDto, SelectMakbuzHareketDto>, IScopedDependency
{
    public Guid CariId { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        IsPopupListPage = true;
        CariId = (Guid)prm[0];
    }
}