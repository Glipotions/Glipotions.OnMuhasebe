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

namespace Glipotions.OnMuhasebe.Blazor.Services.Base;

public abstract class BaseHareketService<TDataGridItem> :
    ICoreHareketService<TDataGridItem>
{
    public IUiMessageService MessageService { get; set; }//Property Injection
    public IGuidGenerator GuidGenerator { get; set; }//property Injection
    public IStringLocalizerFactory StringLocalizerFactory { get; set; }//property Injection    

    public ComponentBase DataGrid { get; set; }
    public IList<TDataGridItem> ListDataSource { get; set; }
    public TDataGridItem SelectedItem { get; set; }
    public IEnumerable<TDataGridItem> SelectedItems { get; set; }
    public bool SelectFirstDataRow { get; set; }
    public bool ShowFilterRow { get; set; }
    public bool ShowGroupPanel { get; set; }
    public bool IsLoaded { get; set; }
    public bool ShowSelectionCheckBox { get; set; }
    public Guid PopupListPageFocusedRowId { get; set; }
    public IList<string> ExcludeListItems { get; set; }
    public TDataGridItem DataSource { get; set; }
    public bool ToolbarCheckBoxVisible { get; set; }
    public bool IsActiveCards { get; set; }

    public string LoadingCaption => throw new NotImplementedException();

    public string LoadingText => throw new NotImplementedException();

    public bool IsPopupListPage { get; set; }
    public bool EditPageVisible { get; set; }
    public string SelectedReportName { get; set; }
    public string BaseReportFolder { get; set; }
    public string ReportFolder { get; set; }
    public Action HasChanged { get; set; }
    public ComponentBase ActiveEditComponent { get; set; }
    public TDataGridItem TempDataSource { get; set; }
    public bool ShowReportSelectBox { get; set; }
    public bool IsGrantedDefault { get; set; }
    public bool IsGrantedCreate { get; set; }
    public bool IsGrantedUpdate { get; set; }
    public bool IsGrantedDelete { get; set; }
    public bool IsGrantedPrint { get; set; }
    public bool IsGrantedReserve { get; set; }


    public void BeforeShowPopupListPage(params object[] prm)
    {
        throw new NotImplementedException();
    }

    public void ButtonEditDeleteKeyDown(IEntityDto entity, string fieldName)
    {
        throw new NotImplementedException();
    }

    public async Task ConfirmMessage(string message, Action action, string title = null)
    {
        var confirmed = await MessageService.Confirm(message, title);

        if (confirmed)
            action();
    }
    /// <ÖZET>
    /// Sayfayı gizler.
    public void HideEditPage()
    {
        EditPageVisible = false;
        HasChanged();
    }

    public void HideListPage()
    {
        throw new NotImplementedException();
    }

    public void SelectEntity(IEntityDto targetEntity)
    {
        throw new NotImplementedException();
    }

    public void SetDataRowSelected(TDataGridItem item)
    {
        ((DxDataGrid<TDataGridItem>)DataGrid).SetDataRowSelected(item, true);
    }
    /// <ÖZET>
    /// 
    /// Eğer gelen parametrenin değeri true ise ilk satıra false ise son satıra focuslanır.
    public void SetDataRowSelected(bool first)
    {
        ((DxDataGrid<TDataGridItem>)DataGrid).SetDataRowSelected(
            first ? ListDataSource.FirstOrDefault() :
            ListDataSource.LastOrDefault(), true);
    }

    public void ShowEditPage()
    {
        throw new NotImplementedException();
    }

    public void ShowListPage(bool firstRender)
    {
        throw new NotImplementedException();
    }

    public void FillTable<TItem>(ICoreHareketService<TItem> hareketService, Action hasChanged)
    {
        throw new NotImplementedException();
    }

    public void AddSelectedItems()
    {
        throw new NotImplementedException();
    }
    /// <ÖZET>
    /// Hareketler tablosunda altta toplamlarını almayı sağlayan fonksiyon.
    public virtual void GetTotal() { }

    public virtual void BeforeInsert() { }

    public virtual void BeforeUpdate()
    {
        DataSource = SelectedItem;
        EditPageVisible = true;
    }
    /// <ÖZET>
    /// Hareketi Silmeyi Sağlayan Async Fonksiyon.
    /// Silmeden önce onay mesajı sorulur ardından onaylanmışsa işlemler yapılmaya başlar
    /// silinecek entitynin id si bulunur. ve silinir ardından reflesh işlemi yapılır.
    /// silindikten sonra bir sonraki satırı seçer. yani aynı index olmuş olur
    /// silme işlemi olduğu için genel toplamlar değişir tekrar hesaplanır.
    /// daha sonra ekrana yansıtmak için haschanged işlemi yapılır.
    public virtual async Task DeleteAsync()
    {
        await ConfirmMessage(L["DeleteConfirmMessage"], async () =>
        {
            var deletedEntityIndex = ListDataSource.FindIndex(x =>
                x.GetEntityId() == SelectedItem.GetEntityId());

            ListDataSource.Remove(SelectedItem);
            await ((DxDataGrid<TDataGridItem>)DataGrid).Refresh();
            SelectedItem = ListDataSource.SetSelectedItem(deletedEntityIndex);
            SetDataRowSelected(SelectedItem);
            GetTotal();
            HasChanged();

        }, L["DeleteConfirmMessageTitle"]);
    }

    public virtual void OnSubmit() { }

    /// <ÖZET>
    /// ((IEntityDto<Guid>)DataSource).Id == Guid.Empty ise insert işlemidir
    ///     insert işlemi ise yeni bir id oluşturulur. GuidGenerator sıralı id verir.
    ///     
    /// else bloğu(update işlemi) id si bulunur ve dataSource bulunan indexteki verinin yerine doldurulur.
    /// toplamlarda değişiklik olduğu için getTotal fonksiyonu çağırılır.
    public virtual void InsertOrUpdate()
    {
        if (((IEntityDto<Guid>)DataSource).Id == Guid.Empty)
        {
            ((IEntityDto<Guid>)DataSource).Id = GuidGenerator.Create();
            ListDataSource.Add(DataSource);
        }
        else
        {
            var itemIndex = ListDataSource.FindIndex(x =>
              ((IEntityDto<Guid>)x).Id == ((IEntityDto<Guid>)SelectedItem).Id);

            ListDataSource[itemIndex] = DataSource;
        }

        EditPageVisible = false;
        SetDataRowSelected(DataSource);
        GetTotal();
    }

    public virtual void Hesapla(object value, string propertyName) { }

    #region Localizer

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