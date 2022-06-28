using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace Glipotions.Blazor.Core.Services;

/// <ÖZET>
/// Data grid için yapılan Servistir. Bu Servis Farklı 3rd firmalarda kullanılabilir.
/// 
/// Datagridden gönderilen itemlerde işlem yapmak için generic yapıldı.
/// TDataGridItem = Datagrid'in 1 satırı yani 1 itemi temsil eder.
/// 
/// DataGrid ComponentBase'den Tanımlanması her 3rd firmanın kullanmasını sağlar. bağımlılık yok
/// 
public interface ICoreDataGridService<TDataGridItem>
{
    public ComponentBase DataGrid { get; set; }
    public IList<TDataGridItem> ListDataSource { get; set; }
    public TDataGridItem SelectedItem { get; set; }
    public IEnumerable<TDataGridItem> SelectedItems { get; set; }
    public bool SelectFirstDataRow { get; set; }
    public bool ShowFilterRow { get; set; }
    public bool ShowGroupPanel { get; set; }
    /// <ÖZET>
    /// Veriler Yüklendi mi Yüklenmedi Mi tutan Prop
    public bool IsLoaded { get; set; }
    public bool ShowSelectionCheckBox { get; set; }
    public Guid PopupListPageFocusedRowId { get; set; }
    /// <ÖZET>
    /// (4/5) 37. video 37.dk
    /// Ödeme belgelerinde takip nolara göre liste
    public IList<string> ExcludeListItems { get; set; }
    /// <ÖZET>
    /// Listeyi görüntüleme prop'u, eğer ilk kez renderlıyorsa ilk satırı seçmesi sağlanır.
    void ShowListPage(bool firstRender);
    /// <ÖZET>
    /// Gönderilen itemin bulunduğu satırı seçer.
    void SetDataRowSelected(TDataGridItem item);
    /// <ÖZET>
    /// Eğer gelen parametrenin değeri true ise ilk satıra false ise son satıra focuslanır.
    void SetDataRowSelected(bool first);
    /// <ÖZET>
    /// Hareket Tablosunu doldurmak için üretilen fonksiyon
    void FillTable<TItem>(ICoreHareketService<TItem> hareketService, Action hasChanged);
    void AddSelectedItems();
}