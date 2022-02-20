using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Faturalar;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Hizmetler;

public class HizmetHareketListParameterDto: PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid HizmetId { get; set; }
    public FaturaHareketTuru HareketTuru { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}