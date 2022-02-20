using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Hizmetler;

public interface IHizmetAppService : ICrudAppService<SelectHizmetDto, ListHizmetDto,
    HizmetListParameterDto, CreateHizmetDto, UpdateHizmetDto, CodeParameterDto>
{
}