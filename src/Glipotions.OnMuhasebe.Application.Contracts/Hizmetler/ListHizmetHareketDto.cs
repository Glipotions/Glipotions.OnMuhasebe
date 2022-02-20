using System;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Hizmetler;

public class ListHizmetHareketDto: EntityDto<Guid>
{
    public Guid? HizmetId { get; set; }
    public Guid? FaturaId { get; set; }
    public DateTime Tarih { get; set; }
    public string BelgeNo { get; set; }
    public FaturaTuru FaturaTuru { get; set; }
    public string BelgeTuru { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public string HareketTuruAdi { get; set; }
    public string Aciklama { get; set; }
    public decimal Miktar { get; set; }
    public string Birim { get; set; }
    public decimal BirimFiyat { get; set; }
    public decimal Tutar { get; set; }
}