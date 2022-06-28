using System;
using Glipotions.OnMuhasebe.BankaHesaplar;
using Glipotions.OnMuhasebe.BankaSubeler;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class BankaSubeService : BaseService<ListBankaSubeDto, SelectBankaSubeDto>,
    IScopedDependency
{
    public Guid BankaId { get; set; }
    /// <ÖZET>
    /// ToolbarCheckBoxVisible = prm.Length == 1 ise listelenme istendiği belli olur, 
    ///     ancak 1 den fazla parametre gönderilirse seçim yapıldığı anlaşılır ve pasif kartların görünmemesi için
    ///     checkbox visible false olur.
    ///     
    /// IsPopupListPage = true yapılır ve özel kod sayfasının açılmasını sağlar.
    ///     if (OzelKodService.IsPopupListPage) =>> Editpagelerdeki bu komuttan dolayı açılır.
    /// 
    /// EntityId prm[0] dan alınır.
    /// 
    /// Atamalar yapıldıktan sonra PopupListPageFocusedRowId ile sayfa açılırken seçili özel kod açılır.
    public override void BeforeShowPopupListPage(params object[] prm)
    {
        ToolbarCheckBoxVisible = prm.Length == 1;
        IsPopupListPage = true;
        BankaId = (Guid)prm[0];
        PopupListPageFocusedRowId = prm.Length > 1 && prm[1] != null ? (Guid)prm[1] : 
            Guid.Empty;
    }
    /// <ÖZET>
    /// BankaHesapta işlem yapılıyorsa BankaSubeId ve Adi
    /// MakbuzHarekette işlem yapılıyorsa CekBankaSubeId ve Adı buttonEditte seçildiğinde oto gelmesi sağlanır.
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectBankaHesapDto bankaHesap:
                bankaHesap.BankaSubeId = SelectedItem.Id;
                bankaHesap.BankaSubeAdi = SelectedItem.Ad;
                break;
            
            case SelectMakbuzHareketDto makbuzHareket:
                makbuzHareket.CekBankaSubeId = SelectedItem.Id;
                makbuzHareket.CekBankaSubeAdi = SelectedItem.Ad;
                break;
        }
    }
}