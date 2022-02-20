using System;
using System.Collections.Generic;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Depolar;

public class ListDepoDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string OzelKod1Adi { get; set; }
    public string OzelKod2Adi { get; set; }
    public decimal Giren { get; set; }
    public decimal Cikan { get; set; }
    public decimal Mevcut => Giren - Cikan;
    public string Aciklama { get; set; }
    public ICollection<SelectFaturaHareketDto> FaturaHareketler { get; set; }
}