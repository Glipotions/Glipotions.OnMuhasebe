using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Stoklar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class StokService : BaseService<ListStokDto, SelectStokDto>, IScopedDependency
{
    /// <ÖZET>
    /// Stok Seçildiğinde StokId, StokKodu, StokAdi, BirimAdi, BirimFiyat, KdvOrani
    /// propertyleri doldurulur.
    public override void SelectEntity(IEntityDto targetEntity)
    {
        if (targetEntity is SelectFaturaHareketDto hareket)
        {
            hareket.StokId = SelectedItem.Id;
            hareket.StokKodu = SelectedItem.Kod;
            hareket.StokAdi = SelectedItem.Ad;
            hareket.BirimAdi = SelectedItem.BirimAdi;
            hareket.BirimFiyat = SelectedItem.BirimFiyat;
            hareket.KdvOrani = SelectedItem.KdvOrani;
        }
    }
}