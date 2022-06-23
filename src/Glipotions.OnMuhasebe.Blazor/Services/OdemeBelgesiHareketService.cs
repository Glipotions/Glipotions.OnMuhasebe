using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.OdemeBelgeleri;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class OdemeBelgesiHareketService : BaseService<ListOdemeBelgesiHareketDto, SelectMakbuzHareketDto>,
    IScopedDependency
{
    public OdemeTuru OdemeTuru { get; set; }
    public Guid EntityId { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        IsPopupListPage = true;
        OdemeTuru = (OdemeTuru)prm[0];
        EntityId = (Guid)prm[1];
    }
}