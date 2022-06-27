using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.OzelKodlar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.OzelKodlar;

public partial class OzelKodListPage
{
    /// <ÖZET>
    /// fonksiyon özel kod'a göre ayarlandı
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new OzelKodListParameterDto
        {
            KodTuru = Service.KodTuru,
            KartTuru = Service.KartTuru,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }
    /// <ÖZET>
    /// 
    /// GetCodeAsync ile Kod alanını KodTuru, KartTuru, Durum ile sıradaki kodu bulup doldurulur.
    ///  KodTuru, KartTuru, Durum doldurulur.
    /// ShowEditPage ile sayfa gösterilir.
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectOzelKodDto
        {
            Kod = await GetCodeAsync(new OzelKodCodeParameterDto
            {
                KodTuru = Service.KodTuru,
                KartTuru = Service.KartTuru,
                Durum = Service.IsActiveCards
            }),
            KodTuru = Service.KodTuru,
            KartTuru = Service.KartTuru,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}