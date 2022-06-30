using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Glipotions.OnMuhasebe.OdemeBelgeleri;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public interface IBankaHesapHareketAppService : ICrudAppService<SelectMakbuzHareketDto, ListOdemeBelgesiHareketDto,
    MakbuzHareketListParameterDto, MakbuzHareketDto, MakbuzHareketDto, MakbuzNoParameterDto>
{
}