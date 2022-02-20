using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Donemler;

public interface IDonemAppService : ICrudAppService<SelectDonemDto, ListDonemDto,
    DonemListParameterDto, CreateDonemDto, UpdateDonemDto, CodeParameterDto>
{
}