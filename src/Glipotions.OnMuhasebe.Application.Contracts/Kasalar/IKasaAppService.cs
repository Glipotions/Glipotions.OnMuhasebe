using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Kasalar;

public interface IKasaAppService : ICrudAppService<SelectKasaDto, ListKasaDto,
    KasaListParameterDto, CreateKasaDto, UpdateKasaDto, KasaCodeParameterDto>
{
}