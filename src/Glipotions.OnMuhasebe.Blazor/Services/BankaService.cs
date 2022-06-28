using Glipotions.OnMuhasebe.BankaHesaplar;
using Glipotions.OnMuhasebe.Bankalar;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class BankaService : BaseService<ListBankaDto, SelectBankaDto>, IScopedDependency
{
    /// <ÖZET>
    /// BankaHesapta işlem yapılıyorsa BankaId ve Adi
    /// MakbuzHarekette işlem yapılıyorsa CekBankaId ve Adı buttonEditte seçildiğinde oto gelmesi sağlanır.
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectBankaHesapDto bankaHesap:
                bankaHesap.BankaId = SelectedItem.Id;
                bankaHesap.BankaAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.CekBankaId = SelectedItem.Id;
                makbuzHareket.CekBankaAdi = SelectedItem.Ad;
                break;
        }
    }
}