using Volo.Abp.Application.Dtos;

namespace Glipotions.OnMuhasebe.CommonDtos;

public class CodeParameterDto : IDurum, IEntityDto
{
    public bool Durum { get; set; }
}