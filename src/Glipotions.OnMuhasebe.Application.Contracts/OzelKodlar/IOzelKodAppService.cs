using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.OzelKodlar;

public interface IOzelKodAppService : ICrudAppService<SelectOzelKodDto, ListOzelKodDto,
    OzelKodListParameterDto, CreateOzelKodDto, UpdateOzelKodDto, OzelKodCodeParameterDto>
{
}