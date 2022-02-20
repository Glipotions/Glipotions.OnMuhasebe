using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.Subeler;

public class SubeListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public bool Durum { get; set; }
}