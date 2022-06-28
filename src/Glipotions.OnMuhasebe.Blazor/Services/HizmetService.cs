using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Hizmetler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class HizmetService : BaseService<ListHizmetDto, SelectHizmetDto>, 
    IScopedDependency
{
    /// <ÖZET>
    /// Hizmet Seçildiğinde FaturaHareketlerinde Hizmete bağlı propertyler doldurulur.
    public override void SelectEntity(IEntityDto targetEntity)
    {
        if (targetEntity is SelectFaturaHareketDto hareket)
        {
            hareket.HizmetId = SelectedItem.Id;
            hareket.HizmetKodu = SelectedItem.Kod;
            hareket.HizmetAdi = SelectedItem.Ad;
            hareket.BirimAdi = SelectedItem.BirimAdi;
            hareket.BirimFiyat = SelectedItem.BirimFiyat;
            hareket.KdvOrani = SelectedItem.KdvOrani;
        }
    }
}