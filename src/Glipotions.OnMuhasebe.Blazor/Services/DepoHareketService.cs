using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Stoklar;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class DepoHareketService : BaseService<ListStokHareketDto, SelectFaturaHareketDto>, IScopedDependency
{
    public Guid DepoId { get; set; }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        DepoId = (Guid)prm[0];
        IsPopupListPage = true;
    }
}