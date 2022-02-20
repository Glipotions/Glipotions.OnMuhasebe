using System;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Cariler;

public class ListCariHareketDto : EntityDto<Guid>
{
    public Guid CariId { get; set; }
    public Guid? FaturaId { get; set; }
    public Guid? MakbuzId { get; set; }
    public DateTime Tarih { get; set; }
    public string BelgeNo { get; set; }
    public string BelgeTuru { get; set; }
    public FaturaTuru FaturaTuru { get; set; }
    public MakbuzTuru MakbuzTuru { get; set; }
    public string HareketTuru { get; set; }
    public string Aciklama { get; set; }
    public decimal Tutar { get; set; }
}