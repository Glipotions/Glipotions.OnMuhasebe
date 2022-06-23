using System;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.OzelKodlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class OzelKodService : BaseService<ListOzelKodDto, SelectOzelKodDto>,
    IScopedDependency
{
    public OzelKodTuru KodTuru { get; private set; }
    public KartTuru KartTuru { get; private set; }

    public override void SelectEntity(IEntityDto targetEntity)
    {
        var ozelKod = (IOzelKod)targetEntity;

        switch (KodTuru)
        {
            case OzelKodTuru.OzelKod1:
                ozelKod.OzelKod1Id = SelectedItem?.Id;
                ozelKod.OzelKod1Adi = SelectedItem?.Ad;
                break;

            case OzelKodTuru.OzelKod2:
                ozelKod.OzelKod2Id = SelectedItem?.Id;
                ozelKod.OzelKod2Adi = SelectedItem?.Ad;
                break;
        }
    }

    public override void BeforeShowPopupListPage(params object[] prm)
    {
        ToolbarCheckBoxVisible = false;
        IsPopupListPage = true;

        KodTuru = (OzelKodTuru)prm[0];
        KartTuru = (KartTuru)prm[1];
        PopupListPageFocusedRowId = prm[2] == null ? Guid.Empty : (Guid)prm[2];
    }

    public override void ButtonEditDeleteKeyDown(IEntityDto entity, string fieldName)
    {
        var ozelKod = (IOzelKod)entity;

        switch (fieldName)
        {
            case nameof(ozelKod.OzelKod1Adi):
                ozelKod.OzelKod1Id = null;
                ozelKod.OzelKod1Adi = null;
                break;

            case nameof(ozelKod.OzelKod2Adi):
                ozelKod.OzelKod2Id = null;
                ozelKod.OzelKod2Adi = null;
                break;
        }
    }
}