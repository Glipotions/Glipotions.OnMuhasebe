using System.Threading.Tasks;

namespace Glipotions.Blazor.Core.Services;
/// <ÖZET>
/// Hareketler için yapılan interface
/// ICoreDataGridService den kalıtım alınır.
/// ICoreEditPageService den kalıtım alınır.
/// ICoreListPageService den kalıtım alınır.
/// ICoreMessageService den kalıtım alınır.
/// ICoreCommonService den kalıtım alınır.
/// </summary>
/// <typeparam name="TDataGridItem"></typeparam>
public interface ICoreHareketService<TDataGridItem> : ICoreDataGridService<TDataGridItem>,
    ICoreEditPageService<TDataGridItem>, ICoreListPageService, ICoreMessageService,
    ICoreCommonService
{
    public TDataGridItem TempDataSource { get; set; }
    /// <ÖZET>
    /// Hareketlerin Toplamını almayı sağlayan fonksiyon.
    void GetTotal();
    /// <ÖZET>
    /// Hareket Eklemeden Önceki İşlemleri Yapan Fonksiyon.
    void BeforeInsert();
    /// <ÖZET>
    /// Hareketi Güncellemeden Önceki İşlemleri Yapan Fonksiyon.
    void BeforeUpdate();
    /// <ÖZET>
    /// Hareket Silmeden Önceki İşlemleri Yapan Fonksiyon.
    /// Delete Async olduğu için Task olarak işaretlendi.
    Task DeleteAsync();
    /// <ÖZET>
    /// (4/5) 28. Video 10. Dk
    void OnSubmit();
    /// <ÖZET>
    /// Insert veya Update durumuna göre id ataması yapan fonksiyon
    void InsertOrUpdate();
    /// <ÖZET>
    /// Veriler değiştiği anda tutarları hesaplamayı sağlayan fonksiyon.
    void Hesapla(object value, string propertyName);
}