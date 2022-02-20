using System;
using System.Collections.Generic;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Faturalar;

public class SelectFaturaDto : EntityDto<Guid>, IOzelKod
{
    public FaturaTuru FaturaTuru { get; set; }
    public string FaturaNo { get; set; }
    public DateTime Tarih { get; set; }
    public Guid CariId { get; set; }
    public string CariAdi { get; set; }
    public string VergiDairesi { get; set; }
    public string VergiNo { get; set; }
    public string Adres { get; set; }
    public string Telefon { get; set; }
    public decimal BrutTutar { get; set; }
    public decimal IndirimTutar { get; set; }
    public decimal KdvHaricTutar { get; set; }
    public decimal KdvTutar { get; set; }
    public decimal NetTutar { get; set; }
    public int HareketSayisi { get; set; }
    public Guid? OzelKod1Id { get; set; }
    public string OzelKod1Adi { get; set; }
    public Guid? OzelKod2Id { get; set; }
    public string OzelKod2Adi { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public List<SelectFaturaHareketDto> FaturaHareketler { get; set; }
}