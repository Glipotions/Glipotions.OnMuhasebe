using Volo.Abp.Application.Dtos;

namespace Glipotions.Blazor.Core.Services;

/// <ÖZET>
/// 
/// ListPage için yapılan Servistir.
public interface ICoreListPageService
{
    /// <ÖZET>
    /// Toolbardaki CheckBox'ın görünürlüğünü ayarlar.
    /// Aktif Pasif Kartlar için Gösterilen Checkbox görevi görür.
    public bool ToolbarCheckBoxVisible { get; set; }
    /// <ÖZET>
    /// Kartlar Aktif mi?
    public bool IsActiveCards { get; set; }
    /// <ÖZET>
    /// CircleLayout Yüklenirkenki Caption'ı Tutan Prop
    public string LoadingCaption { get; }
    /// <ÖZET>
    /// CircleLayout Yüklenirkenki Text'ı Tutan Prop
    public string LoadingText { get; }
    /// <ÖZET>
    /// List Page in Popup olup olmadığını belirten prop.
    /// bu prop'a göre işlemlerin yapılabilir örn: DevListPageLayout.razor dosyası
    public bool IsPopupListPage { get; set; }
    /// <ÖZET>
    /// Popuplarda EditPage in Visible olup olmadığını tutan prop.
    /// örn: DevListPageLayout.razor dosyası
    public bool EditPageVisible { get; set; }
    /// <ÖZET>
    /// Seçilen Raporun Adı
    public string SelectedReportName { get; set; }
    /// <ÖZET>
    /// Rapor'un ana dosyasının yolu örn: Raporlar
    public string BaseReportFolder { get; set; }
    /// <ÖZET>
    /// Rapor dosyasının yolu örn Faturalar
    public string ReportFolder { get; set; }
    /// <ÖZET>
    /// ShowReportSelectBox Rapor seçimi için açılacak olan popup açılması için true yapılır.
    public bool ShowReportSelectBox { get; set; }
    public bool IsGrantedDefault { get; set; }
    public bool IsGrantedCreate { get; set; }
    public bool IsGrantedUpdate { get; set; }
    public bool IsGrantedDelete { get; set; }
    public bool IsGrantedPrint { get; set; }
    public bool IsGrantedReserve { get; set; }
    /// <ÖZET>
    /// Edit veya insert işlemi yaparken sayfayı göstermeye yarar.
    void ShowEditPage();
    /// <ÖZET>
    /// sayfayı gizlemeye yarar.
    void HideEditPage();
    void HideListPage();
    /// <ÖZET>
    /// (3/5) 37. Videoda anlatıldı not alınmadan izlendi
    void SelectEntity(IEntityDto targetEntity);
    /// <ÖZET>
    /// (3/5) 37. Videoda anlatıldı not alınmadan izlendi
    void BeforeShowPopupListPage(params object[] prm);
}