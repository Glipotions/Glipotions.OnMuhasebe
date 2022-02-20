using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Masraflar;

public interface IMasrafHareketAppService: ICrudAppService<SelectFaturaHareketDto,
    ListMasrafHareketDto, MasrafHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}