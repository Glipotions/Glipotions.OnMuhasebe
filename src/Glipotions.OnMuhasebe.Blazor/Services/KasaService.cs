using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Kasalar;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class KasaService : BaseService<ListKasaDto, SelectKasaDto>, IScopedDependency
{
    /// <ÖZET>
    /// Makbuz da işlem yapılıyorsa KasaId ve Adi
    /// MakbuzHarekette işlem yapılıyorsa KasaId ve Adı buttonEditte seçildiğinde oto gelmesi sağlanır.
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