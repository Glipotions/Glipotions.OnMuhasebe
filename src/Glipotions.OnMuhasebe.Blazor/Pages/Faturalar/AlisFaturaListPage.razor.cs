using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Faturalar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Faturalar;

public partial class AlisFaturaListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new FaturaListParameterDto
        {
            FaturaTuru = FaturaTuru.Alis,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    /// <ÖZET>
    /// Alış faturası oluştururken hangi alanların dolu olarak gelmesi gerekiyorsa onlar doldurulur.
    /// await Task.CompletedTask; ile async fonksiyonu bitirilir.
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectFaturaDto
        {
            FaturaTuru = FaturaTuru.Alis,
            Tarih = System.DateTime.Now.Date,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards,
            FaturaHareketler = new List<FaturaHareketler.SelectFaturaHareketDto>()
        };

        Service.ShowEditPage();

        await Task.CompletedTask;
    }
}