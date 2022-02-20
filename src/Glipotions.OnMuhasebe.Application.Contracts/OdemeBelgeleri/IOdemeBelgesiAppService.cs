using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.OdemeBelgeleri;

public interface IOdemeBelgesiAppService : ICrudAppService<SelectMakbuzHareketDto, ListOdemeBelgesiDto,
    OdemeBelgesiListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
}