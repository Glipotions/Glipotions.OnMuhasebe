using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.OdemeBelgeleri;
using DevExpress.Blazor.Internal;

namespace Glipotions.OnMuhasebe.Blazor.Pages.OdemeBelgeleri;

public partial class OdemeBelgeleriListPage
{
    public AppService AppService { get; set; }
    /// <ÖZET>
    /// (4/5) 37. video 23. Dk
    /// 
    /// bu sayfa Hem Popup olarak açılabilir hem ana menüdeki butona basılarak açılabilir.
    ///     !Service.IsPopupListPage kontrolü ile ana menüden açıldığı teyit edilir.
    ///         Eğer Ana menüden açılmışsa MakbuzTürü yok OdemeTurleri çek, senet ve pos olur.
    ///         diğerleri listelenip üzerinde işlem yapılacak türler olmadığı için onlar yazılmadı.
    ///         KendiBelgemiz false yaparak tüm hepsinin gelmesi sağlanır.
    ///         
    /// listDataSource doldurulur
    /// ExcludeListItems => takip noların tutulduğu listedir. (40. dk detay anlatım)
    /// 
    ///         
    /// <returns></returns>
    protected override async Task GetListDataSourceAsync()
    {
        if (!Service.IsPopupListPage)
        {
            Service.MakbuzService.MakbuzTuru = 0;
            Service.OdemeTurleri = $"{(byte)OdemeTuru.Cek}, {(byte)OdemeTuru.Senet}, " +
                                   $"{(byte)OdemeTuru.Pos}";
            Service.KendiBelgemiz = false;
        }

        var listDataSource = (await GetListAsync(new OdemeBelgesiListParameterDto
        {
            Sql = Service.MakbuzService.MakbuzTuru == MakbuzTuru.BankaIslem ||
                  Service.MakbuzService.MakbuzTuru == MakbuzTuru.KasaIslem ? "IslemYapilabilecekTumOdemeBelgeleri" :
                  Service.MakbuzService.MakbuzTuru == MakbuzTuru.Odeme ? "IslemYapilabilecekOdemeBelgeleri" :
                  "OdemeBelgeleri",
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            KendiBelgemiz = Service.KendiBelgemiz,
            OdemeTurleri = Service.OdemeTurleri
        }))?.Items.ToList();

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;

        Service.ExcludeListItems.ForEach(x =>
        {
            var entity = Service.ListDataSource.FirstOrDefault(y => y.TakipNo == x);
            Service.ListDataSource.Remove(entity);
        });

        Service.ExcludeListItems = null;
        Service.IsLoaded = true;
    }
}