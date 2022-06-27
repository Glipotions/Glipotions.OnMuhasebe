using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Depolar;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Hizmetler;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Hizmetler;

public partial class HizmetHareketListPage
{
    public AppService AppService { get; set; }//Property Injection

    /// <ÖZET>
    /// HizmetHareketinde HizmetId, HareketTuru, SubeId ve DonemId dolu olması gerekir.
    protected override async Task GetListDataSourceAsync()
    {
        Service.ListDataSource = (await GetListAsync(new HizmetHareketListParameterDto
        {
            HizmetId = Service.HizmetId,
            HareketTuru = Service.HareketTuru,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
        })).Items.ToList();

        Service.IsLoaded = true;
    }
}