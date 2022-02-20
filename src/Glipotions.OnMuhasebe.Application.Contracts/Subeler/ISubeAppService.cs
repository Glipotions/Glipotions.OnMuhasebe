using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Subeler;

public interface ISubeAppService : ICrudAppService<SelectSubeDto, ListSubeDto,
    SubeListParameterDto, CreateSubeDto, UpdateSubeDto, CodeParameterDto>
{
}