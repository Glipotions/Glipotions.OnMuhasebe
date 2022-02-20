using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.OzelKodlar;

public class SelectOzelKodDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public OzelKodTuru? KodTuru { get; set; }
    public KartTuru? KartTuru { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}