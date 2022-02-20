using System;
using System.Collections.Generic;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Makbuzlar;

public class SelectMakbuzDto : EntityDto<Guid>, IOzelKod
{
    public MakbuzTuru MakbuzTuru { get; set; }
    public string MakbuzNo { get; set; }
    public DateTime Tarih { get; set; }
    public Guid? CariId { get; set; }
    public string CariKodu { get; set; }
    public string CariAdi { get; set; }
    public Guid? KasaId { get; set; }
    public string KasaAdi { get; set; }
    public Guid? BankaHesapId { get; set; }
    public string BankaHesapAdi { get; set; }
    public int HareketSayisi { get; set; }
    public decimal CekToplam { get; set; }
    public decimal SenetToplam { get; set; }
    public decimal PosToplam { get; set; }
    public decimal NakitToplam { get; set; }
    public decimal BankaToplam { get; set; }
    public decimal GenelToplam => CekToplam + SenetToplam + PosToplam + NakitToplam + BankaToplam;
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    public Guid SubeId { get; set; }
    public string SubeAdi { get; set; }
    public Guid DonemId { get; set; }
    public List<SelectMakbuzHareketDto> MakbuzHareketler { get; set; }
}