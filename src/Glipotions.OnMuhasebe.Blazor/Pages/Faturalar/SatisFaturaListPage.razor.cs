using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.Faturalar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Faturalar;

public partial class SatisFaturaListPage
{
    public AppService AppService { get; set; }

    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new FaturaListParameterDto
        {
            FaturaTuru = FaturaTuru.Satis,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards,
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }

    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectFaturaDto
        {
            FaturaNo = await GetCodeAsync(new FaturaNoParameterDto
            {
                FaturaTuru = FaturaTuru.Satis,
                SubeId = AppService.FirmaParametre.SubeId,
                DonemId = AppService.FirmaParametre.DonemId,
                Durum = Service.IsActiveCards
            }),

            FaturaTuru = FaturaTuru.Satis,
            Tarih = System.DateTime.Now.Date,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards,
            FaturaHareketler = new List<FaturaHareketler.SelectFaturaHareketDto>()
        };

        Service.ShowEditPage();
    }
}