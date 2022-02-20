using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Faturalar;

public interface IFaturaAppService : ICrudAppService<SelectFaturaDto, ListFaturaDto,
    FaturaListParameterDto, CreateFaturaDto, UpdateFaturaDto, FaturaNoParameterDto>
{
}