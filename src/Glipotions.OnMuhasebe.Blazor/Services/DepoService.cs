using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Depolar;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Parametreler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class DepoService : BaseService<ListDepoDto, SelectDepoDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectFirmaParametreDto firmaParametre:
                firmaParametre.DepoId = SelectedItem.Id;
                firmaParametre.DepoAdi = SelectedItem.Ad;
                break;

            case SelectFaturaHareketDto faturaHareket:
                faturaHareket.DepoId = SelectedItem.Id;
                faturaHareket.DepoAdi = SelectedItem.Ad;
                break;
        }
    }
}