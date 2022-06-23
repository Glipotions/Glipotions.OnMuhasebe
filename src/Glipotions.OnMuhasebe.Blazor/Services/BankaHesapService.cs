using Glipotions.Blazor.Core.Models;
using Glipotions.OnMuhasebe.BankaHesaplar;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

/// <ÖZET>
/// BaseService'e istenilen parametreler gönderilir.
/// IScopedDependency den kalıtım alındı Çünkü bu servise birden fazla müracat edildiği için kullanıldı
/// (3/5) 27. Video 5.dk

public class BankaHesapService : BaseService<ListBankaHesapDto, SelectBankaHesapDto>,
    IScopedDependency
{
    public BankaHesapTuru? HesapTuru { get; set; }

    public void BankaHesapTuruSelectedItemChanged(ComboBoxEnumItem<BankaHesapTuru> 
        selectedItem)
    {
        DataSource.HesapTuru = selectedItem.Value;
    }
    
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.BankaHesapId = SelectedItem.Id;
                makbuzHareket.BankaHesapAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzDto makbuz:
                makbuz.BankaHesapId = SelectedItem.Id;
                makbuz.BankaHesapAdi = SelectedItem.Ad;
                break;
        }
    }
}