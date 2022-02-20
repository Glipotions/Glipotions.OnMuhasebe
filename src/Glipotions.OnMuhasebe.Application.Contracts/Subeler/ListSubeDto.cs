using System;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Subeler;

public class ListSubeDto : EntityDto<Guid>
{
    public string Kod { get; set; }
    public string Ad { get; set; }
    public string Aciklama { get; set; }
}