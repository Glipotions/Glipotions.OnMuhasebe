using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Cariler;

public interface ICariHareketAppService : ICrudAppService<SelectMakbuzHareketDto, ListCariHareketDto,
CariHareketListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
    
}