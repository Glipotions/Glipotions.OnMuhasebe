using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.BankaHesaplar;
using Glipotions.OnMuhasebe.Blazor.Services;

namespace Glipotions.OnMuhasebe.Blazor.Pages.BankaHesaplar;

public partial class BankaHesapListPage
{
    public AppService AppService { get; set; }

    /// <ÖZET>
    /// listDataSource HesapTuru, SubeId ve Durum'a göre doldurulur.
    /// SubeId nin doldurulmasının nedeni kullanıcı banka hesap eklediğinde hangi şubede olduğunu belirtmesi
    ///     gerektiğinden subeId nin dolu olması gerekir, geri kalanı kullanıcı doldurur.
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new BankaHesapListParameterDto
        {
            HesapTuru = Service.HesapTuru,
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;
        Service.HesapTuru = null;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    /// <ÖZET>
    /// Kod alanının SubeId ve Durum'a göre doldurulur.
    /// HesapTuru ve SubeId nin doldurulmasının nedeni kullanıcı banka hesap eklediğinde hangi şubede olduğunu belirtmesi
    ///     gerektiğinden subeId nin dolu olması gerekir, geri kalanı kullanıcı doldurur.
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectBankaHesapDto
        {
            Kod = await GetCodeAsync(new BankaHesapCodeParameterDto
            {
                SubeId = AppService.FirmaParametre.SubeId,
                Durum = Service.IsActiveCards
            }),
            HesapTuru = BankaHesapTuru.VadesizMevduatHesabi,
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}