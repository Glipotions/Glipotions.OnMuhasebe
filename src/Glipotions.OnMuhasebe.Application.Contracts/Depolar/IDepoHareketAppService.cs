using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Services;
using Glipotions.OnMuhasebe.Stoklar;

namespace Glipotions.OnMuhasebe.Depolar;

public interface IDepoHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListStokHareketDto, DepoHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
    
}