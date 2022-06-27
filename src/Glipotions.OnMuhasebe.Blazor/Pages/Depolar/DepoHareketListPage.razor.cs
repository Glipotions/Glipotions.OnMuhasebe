using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Depolar;
using Glipotions.OnMuhasebe.MakbuzHareketler;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Depolar;

public partial class DepoHareketListPage
{
    public AppService AppService { get; set; }//Property Injection
    
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new DepoHareketListParameterDto
        {
            DepoId = Service.DepoId,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}