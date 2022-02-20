using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Donemler;

public class SelectDonemDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
    public bool Durum { get; set; }
}