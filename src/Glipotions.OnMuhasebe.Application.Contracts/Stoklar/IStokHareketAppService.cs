using Glipotions.OnMuhasebe.FaturaHareketler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Stoklar;

public interface IStokHareketAppService : ICrudAppService<SelectFaturaHareketDto,
    ListStokHareketDto, StokHareketListParameterDto, FaturaHareketDto, FaturaHareketDto,
    FaturaNoParameterDto>
{
}