using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Masraflar;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class MasrafHareketService : BaseService<ListMasrafHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid MasrafId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        HareketTuru = (FaturaHareketTuru)prm[0];
        MasrafId = (Guid)prm[1];
        IsPopupListPage = true;
    }
}