using System.Linq;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.OdemeBelgeleri;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class OdemeBelgesiService : BaseService<ListOdemeBelgesiDto, SelectMakbuzHareketDto>,
    IScopedDependency
{
    public AppService AppService { get; set; }
    public MakbuzService MakbuzService { get; set; }
    public MakbuzHareketService MakbuzHareketService { get; set; }
    public string OdemeTurleri { get; set; }
    public bool KendiBelgemiz { get; set; }

    /// <ÖZET>
    /// (5/5) 5. video dk 20
    /// Amaç: Ödeme belgeleri list pageden 1 veya 1 den fazla makbuz seçip bunlar makbuza eklendiği anda
    ///     2. kez o butona basıp seçim yapılacağı zaman makbuza eklenmiş olanları görmemeyi sağlayan fonksiyon
    ///     
    /// ÖZET: LİSTE DIŞI KAYITLAR
    public void CiroEdilebilecekBelgeler(string odemeTurleri)
    {
        BeforeShowPopupListPage();
        OdemeTurleri = odemeTurleri;
        ShowSelectionCheckBox = true;
        ExcludeListItems = MakbuzHareketService.ListDataSource
            .Where(x => x.BelgeDurumu == BelgeDurumu.CiroEdildi ||
                        x.BelgeDurumu == BelgeDurumu.TahsilEdildi ||
                        x.BelgeDurumu == BelgeDurumu.Odendi).Select(x => x.TakipNo).ToList();
    }
    /// <ÖZET>
    /// Seçili Ürünleri Makbuz belgesine ekleme fonksiyonu
    /// Her eklenilen item için yeni bir id oluşturmak gerekiyor.
    /// BelgeDurumu, KasaId, BankaHesapId
    /// </summary>
    public override void AddSelectedItems()
    {
        if (SelectedItems == null) return;

        foreach (var item in SelectedItems)
        {
            item.Id = GuidGenerator.Create();
            item.BelgeDurumu = (MakbuzService.MakbuzTuru == MakbuzTuru.KasaIslem ||
                                MakbuzService.MakbuzTuru == MakbuzTuru.BankaIslem) &&
                               item.KendiBelgemiz ? BelgeDurumu.Odendi :
                (MakbuzService.MakbuzTuru == MakbuzTuru.KasaIslem ||
                 MakbuzService.MakbuzTuru == MakbuzTuru.BankaIslem) &&
                !item.KendiBelgemiz ? BelgeDurumu.TahsilEdildi : BelgeDurumu.CiroEdildi;

            item.KasaId = MakbuzService.MakbuzTuru == MakbuzTuru.KasaIslem ? 
                MakbuzService.DataSource.KasaId : null;
            
            item.BankaHesapId = MakbuzService.MakbuzTuru == MakbuzTuru.BankaIslem ? 
                MakbuzService.DataSource.BankaHesapId : null;

            var mappedDto = ObjectMapper.Map<ListOdemeBelgesiDto, SelectMakbuzHareketDto>(item);
            mappedDto.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)mappedDto.OdemeTuru}"];
            mappedDto.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)mappedDto.BelgeDurumu}"];
            MakbuzHareketService.ListDataSource.Add(mappedDto);
        }
        
        HideListPage();
        MakbuzHareketService.SetDataRowSelected(false);
        MakbuzHareketService.GetTotal();
    }
}