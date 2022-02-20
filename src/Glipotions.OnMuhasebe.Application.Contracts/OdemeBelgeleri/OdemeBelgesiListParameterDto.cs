using System;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.OdemeBelgeleri;

public class OdemeBelgesiListParameterDto : PagedResultRequestDto, IDurum, IEntityDto
{
    public string Sql { get; set; }
    public Guid SubeId { get; set; }
    public Guid DonemId { get; set; }
    public bool KendiBelgemiz { get; set; }
    public string OdemeTurleri { get; set; }
    public bool Durum { get; set; }
}