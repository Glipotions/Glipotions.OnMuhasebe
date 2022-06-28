using System;
using System.Linq;
using Glipotions.Blazor.Core.Models;
using Glipotions.OnMuhasebe.Blazor.Helpers;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class FaturaHareketService : BaseHareketService<SelectFaturaHareketDto>,
    IScopedDependency
{
    public AppService AppService { get; set; }//property Injection
    public FaturaService FaturaService { get; set; }//property Injection

    /// <ÖZET>
    /// Hareket Eklemeden Önce yapılması gereken işlemler
    /// DefaultDepo seçiliyse onu doldurur. Hareket türünü seçer
    /// Sayfanın görünürlüğünü aktif ederek işlemi bitirir.
    public override void BeforeInsert()
    {
        DataSource = new SelectFaturaHareketDto
        {
            DepoId = AppService.FirmaParametre.DepoId,
            DepoAdi = AppService.FirmaParametre.DepoAdi,
            HareketTuru = FaturaHareketTuru.Stok
        };

        EditPageVisible = true;
    }
    /// <ÖZET>
    /// Hareketlerde Tutarların Toplamını hesaplayan Fonksiyon.
    public override void GetTotal()
    {
        FaturaService.DataSource.BrutTutar = ListDataSource.Sum(x => x.BrutTutar);
        FaturaService.DataSource.IndirimTutar = ListDataSource.Sum(x => x.IndirimTutar);
        FaturaService.DataSource.KdvHaricTutar = ListDataSource.Sum(x => x.KdvHaricTutar);
        FaturaService.DataSource.KdvTutar = ListDataSource.Sum(x => x.KdvTutar);
        FaturaService.DataSource.NetTutar = ListDataSource.Sum(x => x.NetTutar);
        FaturaService.DataSource.HareketSayisi = ListDataSource.Count;
    }
    /// <ÖZET>
    /// ilk olarak Validation işlemleri yapılır (TempDataSource a göre)
    /// Geçerli ise TempDataSource ü DataSource a atar
    /// DataSource.HareketTuruAdi doldurulması gerekiyor çünkü tempDataSource de boştur.
    /// Eğer Geçersiz ise Hata mesajı verir.
    public override void OnSubmit()
    {
        var validator = new SelectFaturaHareketDtoValidator(L);
        var result = validator.Validate(TempDataSource);

        if (result.IsValid)
        {
            DataSource = TempDataSource;
            DataSource.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:" +
                $"{ (byte)DataSource.HareketTuru }"];
            InsertOrUpdate();
            HasChanged();
        }
        else
            MessageService.Error(result.Errors.CreateValidationErrorMessage(L));
    }
    /// <ÖZET>
    /// (4/5) 29. video 8.dk
    /// Comboboxta Seçilen harekete göre değer ataması.
    /// Hareket Türü değiştirildiğinde default değerlerine atanır.(null veya 0)
    /// 
    /// Hareket Türü Stok ise Depo ataması yapılır. değilse depo da defaultlanır.
    public void FaturaHareketTuruSelectedItemChanged(
        ComboBoxEnumItem<FaturaHareketTuru> selectedItem, Action hasChanged)
    {
        TempDataSource.HareketTuru = selectedItem.Value;
        hasChanged();

        TempDataSource.StokId = null;
        TempDataSource.StokAdi = null;
        TempDataSource.StokKodu = null;
        TempDataSource.HizmetId = null;
        TempDataSource.HizmetAdi = null;
        TempDataSource.HizmetKodu = null;
        TempDataSource.MasrafId = null;
        TempDataSource.MasrafAdi = null;
        TempDataSource.MasrafKodu = null;
        TempDataSource.BirimFiyat = 0;
        TempDataSource.KdvOrani = 0;

        if (TempDataSource.HareketTuru == FaturaHareketTuru.Stok)
        {
            TempDataSource.DepoId = AppService.FirmaParametre.DepoId;
            TempDataSource.DepoAdi = AppService.FirmaParametre.DepoAdi;
        }
        else
        {
            TempDataSource.DepoId = null;
            TempDataSource.DepoAdi = null;
        }
    }
    /// <ÖZET>
    /// (4/5) 29. video 32. dk
    /// Veriler değiştiği anda tutarları hesaplamayı sağlayan fonksiyon.
    /// Property adına göre değişen value atanır ve hesaplamalar yapılır
    /// örn propertName miktar yeni value değeri 
    ///     TempDataSource.GetType().GetProperty(propertyName).SetValue(TempDataSource, value);
    ///     ile atanır ve doğru değerlerle hesaplamalar yapılmış olur.
    public override void Hesapla(object value, string propertyName)
    {
        TempDataSource.GetType().GetProperty(propertyName)
            .SetValue(TempDataSource, value);

        TempDataSource.BrutTutar = TempDataSource.Miktar * TempDataSource.BirimFiyat;

        TempDataSource.IndirimTutar = TempDataSource.IndirimTutar >
            TempDataSource.BrutTutar ? TempDataSource.BrutTutar :
            TempDataSource.IndirimTutar;

        TempDataSource.KdvHaricTutar = (TempDataSource.Miktar *
            TempDataSource.BirimFiyat) - TempDataSource.IndirimTutar;

        TempDataSource.KdvTutar = TempDataSource.KdvHaricTutar *
            TempDataSource.KdvOrani / 100;

        TempDataSource.NetTutar = TempDataSource.KdvHaricTutar + TempDataSource.KdvTutar;
    }
}