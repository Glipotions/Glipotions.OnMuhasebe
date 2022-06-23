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

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        ToolbarCheckBoxVisible = prm.Length == 1;
        IsPopupListPage = true;
        BankaId = (Guid)prm[0];
        PopupListPageFocusedRowId = prm.Length > 1 && prm[1] != null ? (Guid)prm[1] : 
            Guid.Empty;
    }

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