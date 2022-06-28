using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.Blazor.Core.Helpers;
using Glipotions.Blazor.Core.Services;
using Glipotions.OnMuhasebe.Localization;
using DevExpress.Blazor;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Messages;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;

namespace Glipotions.OnMuhasebe.Blazor.Services.Base;

/// <ÖZET>
/// Core Katmanında oluşturduğumuz Servislerden Kalıtım Alır.
/// </summary>
/// <typeparam name="TDataGridItem">    ICoreDataGridService da kullanılır.
///     Liste Gönderilir Örn: ListBankaHesapDto    
/// 
/// <typeparam name="TDataSource">      ICoreEditPageService da kullanılır.
///     Tek Kayıt Gönderilir. örn: SelectBankaHesapDto
public abstract class BaseService<TDataGridItem, TDataSource> :
    ICoreDataGridService<TDataGridItem>, ICoreEditPageService<TDataSource>,
    ICoreListPageService, ICoreMessageService, ICoreCommonService
    where TDataGridItem : class, IEntityDto<Guid>
    where TDataSource : class, new()
{
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }//property Injection    
    /// <ÖZET>
    /// Abp'nin Mesaj için Özel Hazırladığı Propertydir.
    /// ConfirmMessage da kullanıldı.
    public IUiMessageService MessageService { get; set; }//property Injection
    public IGuidGenerator GuidGenerator { get; set; }//property Injection
    public IObjectMapper ObjectMapper { get; set; }//property Injection

    public ComponentBase DataGrid { get; set; }
    public IList<TDataGridItem> ListDataSource { get; set; }
    /// <ÖZET>
    /// DataGridde bind edilerek geriye seçilen item yani kart döndürülür.
    /// kartın id vb. propları bunun içinde gelmiş olur.
    public TDataGridItem SelectedItem { get; set; }
    public IEnumerable<TDataGridItem> SelectedItems { get; set; }
    public bool SelectFirstDataRow { get; set; }
    public bool ShowFilterRow { get; set; }
    public bool ShowGroupPanel { get; set; }
    public bool IsLoaded { get; set; }
    public bool ToolbarCheckBoxVisible { get; set; } = true;
    public bool IsActiveCards { get; set; } = true;
    /// <ÖZET>
    /// Program Başında Kullanılan Splash Screen Ekranındaki Caption
    /// Lütfen Bekleyin. Localize Edilerek
    public string LoadingCaption => L["PleaseWait"];
    /// <ÖZET>
    /// Program Başında Kullanılan Splash Screen Ekranındaki Text
    /// Yükleniyor.. Localize Edilerek.
    public string LoadingText => L["Loading"];
    public bool IsPopupListPage { get; set; }
    public bool EditPageVisible { get; set; }
    public string SelectedReportName { get; set; }
    public string BaseReportFolder { get; set; }="";
    //public string BaseReportFolder { get; set; } = nameof(Reports);
    public string ReportFolder { get; set; }
    public Action HasChanged { get; set; }
    public ComponentBase ActiveEditComponent { get; set; }
    public bool ShowSelectionCheckBox { get; set; }
    public TDataSource DataSource { get; set; }
    public Guid PopupListPageFocusedRowId { get; set; }
    public IList<string> ExcludeListItems { get; set; }
    public bool ShowReportSelectBox { get; set; }
    public bool IsGrantedDefault { get; set; }
    public bool IsGrantedCreate { get; set; }
    public bool IsGrantedUpdate { get; set; }
    public bool IsGrantedDelete { get; set; }
    public bool IsGrantedPrint { get; set; }
    public bool IsGrantedReserve { get; set; }
    /// <ÖZET>
    /// Onay mesajı ve onay olumlu ise action çalıştırılır.
    public async Task ConfirmMessage(string message, Action action, string title = null)
    {
        var confirmed = await MessageService.Confirm(message, title);

        if (confirmed)
            action();
    }
    /// <ÖZET>
    /// 
    /// eğer ilk kez renderlanıyorsa ilk satırı seçer.
    /// 
    /// PopupListPageFocusedRowId != Guid.Empty ise ilk satırı seçmemesini sağlar ve
    ///     seçili entity işaretlenir
    /// 
    /// ilk satır seçilmişse datasource un ilk itemi seçilir ve boş değilse ve 
    ///     checkbox kolon'u gösterilmiyorsa o zaman seçilen itemi işaretle.
    ///     
    public void ShowListPage(bool firstRender)
    {
        if (firstRender)
        {
            SelectFirstDataRow = true;
            return;
        }

        if (PopupListPageFocusedRowId != Guid.Empty)
        {
            SelectFirstDataRow = false;
            SelectedItem = ListDataSource.GetEntityById(PopupListPageFocusedRowId);
            PopupListPageFocusedRowId = Guid.Empty;
        }

        if (SelectFirstDataRow)
        {
            var item = ListDataSource.FirstOrDefault();
            if (item != null && !ShowSelectionCheckBox)
                SetDataRowSelected(item);
        }
        else
            SetDataRowSelected(SelectedItem);
    }
    /// <ÖZET>
    /// Gönderilen DataGrid'de gönderilen item'i(satır) seçer.
    public void SetDataRowSelected(TDataGridItem item)
    {
        ((DxDataGrid<TDataGridItem>)DataGrid).SetDataRowSelected(item, true);
    }
    /// <ÖZET>
    /// SelectFirstDataRow edit page olduğu için seçili row gelmesi için bu özellik false işaretlenir.
    /// EditPageVisible sayfanın görünürlüğünü aktif edip HasChanged ile ekrana yansıtılır.
    /// </summary>
    public void ShowEditPage()
    {
        SelectFirstDataRow = false;
        EditPageVisible = true;
        HasChanged();
    }
    /// <ÖZET>
    /// Sayfanın görünürlüğünü false yapıp HasChanged ile Ekrana Yansıtır.
    public void HideEditPage()
    {
        EditPageVisible = false;
        HasChanged();
    }
    /// <ÖZET>
    /// Popup list page'in görünümünü false yapar
    /// Checkboxların görünümünü false yapar
    /// Seçili itemleri boşaltıyor
    /// ((DxTextBox)ActiveEditComponent)?.FocusAsync() ile Textbox'a focuslanmayı sağlar.
    /// (4/5) 5. Video dk 44.
    public void HideListPage()
    {
        IsPopupListPage = false;
        ShowSelectionCheckBox = false;
        SelectedItems = null;
        ((DxTextBox)ActiveEditComponent)?.FocusAsync();
    }
    /// <ÖZET>
    /// (3/5) son videoda anlatıldı.
    public virtual void SelectEntity(IEntityDto targetEntity) { }
    /// <ÖZET>
    /// (3/5) son videoda anlatıldı.
    public virtual void BeforeShowPopupListPage(params object[] prm)
    {
        ToolbarCheckBoxVisible = false;
        IsPopupListPage = true;

        if (prm.Length > 0)
            PopupListPageFocusedRowId = prm[0] == null ? Guid.Empty : (Guid)prm[0];
    }
    /// <ÖZET>
    /// (3/5) son videoda anlatıldı.
    public virtual void ButtonEditDeleteKeyDown(IEntityDto entity, string fieldName) { }
    /// <ÖZET>
    /// 
    /// Eğer gelen parametrenin değeri true ise ilk satıra false ise son satıra focuslanır.
    public void SetDataRowSelected(bool first)
    {
        ((DxDataGrid<TDataGridItem>)DataGrid).SetDataRowSelected(
            first ? ListDataSource.FirstOrDefault() :
            ListDataSource.LastOrDefault(), true);
    }

    public virtual void FillTable<TItem>(ICoreHareketService<TItem> hareketService,
       Action hasChanged) { }

    public virtual void AddSelectedItems() { }

    #region Localizer
    /// <ÖZET>
    /// (3/5) 30. Video dk 24
    /// Localize işlemi, LoadingText ve Loading Caption proplarını doldurmak için kullanılır.
    private IStringLocalizer _localizer;
    public IStringLocalizer L
    {
        get
        {
            if (_localizer == null)
                _localizer = StringLocalizerFactory.Create(typeof(OnMuhasebeResource));

            return _localizer;
        }
    }

    #endregion
}