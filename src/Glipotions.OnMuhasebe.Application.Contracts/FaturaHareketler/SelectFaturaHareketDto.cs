using System;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.FaturaHareketler;

public class SelectFaturaHareketDto : EntityDto<Guid>
{
    public Guid FaturaId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public string HareketTuruAdi { get; set; }
    public string HareketAdi { get; set; }
    public string HareketKodu { get; set; }
    public int HareketSayisi { get; set; }
    public Guid? StokId { get; set; }
    public string StokKodu { get; set; }
    public string StokAdi { get; set; }
    public Guid? HizmetId { get; set; }
    public string HizmetKodu { get; set; }
    public string HizmetAdi { get; set; }
    public Guid? MasrafId { get; set; }
    public string MasrafKodu { get; set; }
    public string MasrafAdi { get; set; }
    public Guid? DepoId { get; set; }
    public string DepoKodu { get; set; }
    public string DepoAdi { get; set; }
    public string BirimAdi { get; set; }
    public decimal Miktar { get; set; }
    public decimal BirimFiyat { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public int KdvOrani { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public string Aciklama { get; set; }   
}