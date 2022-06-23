using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Stoklar;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class StokHareketService: BaseService<ListStokHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid StokId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        HareketTuru = (FaturaHareketTuru)prm[0];
        StokId = (Guid)prm[1];
        IsPopupListPage = true;
    }
}