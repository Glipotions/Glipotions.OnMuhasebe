using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Stoklar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Stoklar;

public partial class StokHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new StokHareketListParameterDto
        {
            StokId = Service.StokId,
            HareketTuru = Service.HareketTuru,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}