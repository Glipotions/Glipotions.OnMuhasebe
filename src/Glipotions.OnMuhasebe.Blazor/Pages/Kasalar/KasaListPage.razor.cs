using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Kasalar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Kasalar;

public partial class KasaListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new KasaListParameterDto
        {
            SubeId = AppService.FirmaParametre.SubeId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectKasaDto
        {
            Kod = await GetCodeAsync(new KasaCodeParameterDto
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