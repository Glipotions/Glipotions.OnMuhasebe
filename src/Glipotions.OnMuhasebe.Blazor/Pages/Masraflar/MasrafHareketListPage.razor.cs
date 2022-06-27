using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Masraflar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Masraflar;

public partial class MasrafHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new MasrafHareketListParameterDto
        {
            MasrafId = Service.MasrafId,
            HareketTuru = Service.HareketTuru,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}