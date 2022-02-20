using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Raporlar;

public class FinansalHareketDto : IEntityDto
{
    public DateTime Tarih { get; set; }
    public string MakbuzNo { get; set; }
    public decimal Borc { get; set; }
    public decimal Alacak { get; set; }
    public string Aciklama { get; set; }
}