using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Hizmetler;

public interface IHizmetHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListHizmetHareketDto, HizmetHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}