using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Blazor.Services;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Makbuzlar;

public partial class TahsilatMakbuzListPage
{
    public AppService AppService { get; set; }//property injection
    /// <ÖZET>
    /// Makbuzları çekerken gerekli parametreleri doldurur.
    protected override async Task GetListDataSourceAsync()
    {
        var listDataSource = (await GetListAsync(new MakbuzListParameterDto
        {
            MakbuzTuru = MakbuzTuru.Tahsilat,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards
        }))?.Items.ToList();

        Service.IsLoaded = true;

        if (listDataSource != null)
            Service.ListDataSource = listDataSource;
    }
    /// <ÖZET>
    /// Insert sayfası açılmadan önce dolu olması gereken propertyleri dolduran fonksiyon.
    protected override async Task BeforeInsertAsync()
    {
        Service.DataSource = new SelectMakbuzDto
        {
            MakbuzNo = await GetCodeAsync(new MakbuzNoParameterDto
            {
                MakbuzTuru = MakbuzTuru.Tahsilat,
                SubeId = AppService.FirmaParametre.SubeId,
                DonemId = AppService.FirmaParametre.DonemId,
                Durum = Service.IsActiveCards
            }),

            MakbuzTuru = MakbuzTuru.Tahsilat,
            Tarih = DateTime.Now.Date,
            SubeId = AppService.FirmaParametre.SubeId,
            DonemId = AppService.FirmaParametre.DonemId,
            Durum = Service.IsActiveCards,
            MakbuzHareketler = new List<SelectMakbuzHareketDto>()
        };

        Service.ShowEditPage();
        await Task.CompletedTask;
    }
}