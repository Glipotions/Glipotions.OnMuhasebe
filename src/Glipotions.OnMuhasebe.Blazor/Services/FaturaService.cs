using System;
using System.Collections.Generic;
using Glipotions.Blazor.Core.Services;
using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class FaturaService : BaseService<ListFaturaDto, SelectFaturaDto>,
    IScopedDependency
{
    /// <ÖZET>
    /// Hareketler Tablosunun dolmasını sağlayan fonksiyon.
    /// ListDataSource ü DataSource.FaturaHareketler ile doldur eğer boş ise yeni oluştur.
    /// .GetTotal() ile veriler yüklendikten sonra alttoplamların alınmasını sağlar.
    public override void FillTable<TItem>(ICoreHareketService<TItem> hareketService,
        Action hasChanged)
    {
        if (hareketService is FaturaHareketService faturaHareketService)
        {
            faturaHareketService.ListDataSource = DataSource.FaturaHareketler ??
                new List<FaturaHareketler.SelectFaturaHareketDto>();
            faturaHareketService.HasChanged = hasChanged;
            faturaHareketService.GetTotal();
        }
    }
}