using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Cariler;

/// <Özet>
/// Hareket Sorgulamak için CariId, SubeId ve DonemId gerekiyor.
public class CariHareketListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public Guid CariId { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool Durum { get; set; }
}