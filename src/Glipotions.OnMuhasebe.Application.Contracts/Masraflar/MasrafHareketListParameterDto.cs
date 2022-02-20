using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Masraflar;

public class MasrafHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid MasrafId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}