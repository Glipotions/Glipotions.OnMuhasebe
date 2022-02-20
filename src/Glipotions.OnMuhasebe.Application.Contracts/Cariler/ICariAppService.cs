using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Cariler;

public interface ICariAppService : ICrudAppService<SelectCariDto, ListCariDto,
    CariListParameterDto, CreateCariDto, UpdateCariDto, CodeParameterDto>
{
}