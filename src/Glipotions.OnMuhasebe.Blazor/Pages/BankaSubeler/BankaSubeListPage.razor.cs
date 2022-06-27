using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.BankaSubeler;

namespace Glipotions.OnMuhasebe.Blazor.Pages.BankaSubeler;

public partial class BankaSubeListPage
{
    /// <ÖZET>
    /// listDataSource BankaId ve Durum'a göre doldurulur.
    /// BankaId nin doldurulmasının nedeni kullanıcı banka şube eklediğinde zaten bir bankanın
    ///     şubesini ekleyeceğinden dolayı bankaId nin dolu olması gerekir, geri kalanı kullanıcı doldurur.
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new BankaSubeListParameterDto
        {
            BankaId = Service.BankaId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }
    /// <ÖZET>
    /// 
    /// GetCodeAsync ile Kod alanını BankaId, Durum ile sıradaki kodu bulup doldurulur.
    ///   BankaId, Durum doldurulur.
    ///  BankaId nin doldurulmasının nedeni kullanıcı banka şube eklediğinde zaten bir bankanın
    ///     şubesini ekleyeceğinden dolayı bankaId nin dolu olması gerekir, geri kalanı kullanıcı doldurur.
    /// ShowEditPage ile sayfa gösterilir.
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectBankaSubeDto
        {
            Kod = await GetCodeAsync(new BankaSubeCodeParameterDto
            {
                BankaId = Service.BankaId,
                Durum = Service.IsActiveCards
            }),
            BankaId = Service.BankaId,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}