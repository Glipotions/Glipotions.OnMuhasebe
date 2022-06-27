//using System.Linq;
//using System.Threading.Tasks;
//using Glipotions.OnMuhasebe.Blazor.Services;
//using Glipotions.OnMuhasebe.Faturalar;
//using Glipotions.OnMuhasebe.MakbuzHareketler;

//namespace Glipotions.OnMuhasebe.Blazor.Pages.BankaHesaplar;

//public partial class BankaHesapHareketListPage
//{
//    public AppService AppService { get; set; }//Property Injection
    
//    protected override async Task GetListDataSourceAsync()
//    {
//        Service.ListDataSource = (await GetListAsync(new MakbuzHareketListParameterDto
//        {
//            OdemeTuru = Service.OdemeTuru,
//            EntityId = Service.EntityId,
//            SubeId = AppService.FirmaParametre.SubeId,
//            DonemId = AppService.FirmaParametre.DonemId,
//        })).Items.ToList();

//        Service.IsLoaded = true;
//    }
//}