using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Depolar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Depolar;

public partial class DepoListPage
{
    public AppService AppService { get; set; }

    /// <ÖZET>
    /// SubeId ve Durum a göre depoları listeler. yüklendi olarak işaretler
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new DepoListParameterDto
        {
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    /// <ÖZET>
    /// Kod, SubeId ve Durum propertyleri dolu olarak gelmesi gerektiği için bu işlem yapıldı
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectDepoDto
        {
            Kod = await GetCodeAsync(new DepoCodeParameterDto
            {
                SubeId = AppService.FirmaParametre.SubeId,
                Durum = Service.IsActiveCards
            }),
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        };

        Service.ShowEditPage();
    }
}