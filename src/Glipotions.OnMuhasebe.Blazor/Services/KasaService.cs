using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Kasalar;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class KasaService : BaseService<ListKasaDto, SelectKasaDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.KasaId = SelectedItem.Id;
                makbuzHareket.KasaAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzDto makbuz:
                makbuz.KasaId = SelectedItem.Id;
                makbuz.KasaAdi = SelectedItem.Ad;
                break;
        }
    }
}