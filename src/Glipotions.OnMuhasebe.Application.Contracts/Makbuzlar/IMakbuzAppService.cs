using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Makbuzlar;

public interface IMakbuzAppService : ICrudAppService<SelectMakbuzDto, ListMakbuzDto,
    MakbuzListParameterDto, CreateMakbuzDto, UpdateMakbuzDto, MakbuzNoParameterDto>
{
}