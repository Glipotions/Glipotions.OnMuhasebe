using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Donemler;

public class DonemListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}