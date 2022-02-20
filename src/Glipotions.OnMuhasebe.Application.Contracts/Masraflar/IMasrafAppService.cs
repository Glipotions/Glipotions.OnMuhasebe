using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Masraflar;

public interface IMasrafAppService : ICrudAppService<SelectMasrafDto, ListMasrafDto,
    MasrafListParameterDto, CreateMasrafDto, UpdateMasrafDto, CodeParameterDto>
{
}