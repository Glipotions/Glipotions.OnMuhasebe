using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Depolar;

public interface IDepoAppService : ICrudAppService<SelectDepoDto, ListDepoDto,
    DepoListParameterDto, CreateDepoDto, UpdateDepoDto, DepoCodeParameterDto>
{
}