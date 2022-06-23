using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Hizmetler;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class HizmetHareketService: BaseService<ListHizmetHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid HizmetId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        HareketTuru = (FaturaHareketTuru)prm[0];
        HizmetId = (Guid)prm[1];
        IsPopupListPage = true;
    }
}