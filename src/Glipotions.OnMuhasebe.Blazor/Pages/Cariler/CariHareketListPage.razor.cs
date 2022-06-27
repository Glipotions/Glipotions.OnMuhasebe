using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Cariler;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Cariler;

public partial class CariHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new CariHareketListParameterDto
        {
            CariId = Service.CariId,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}