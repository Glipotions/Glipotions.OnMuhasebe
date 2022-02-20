using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Stoklar;

public interface IStokAppService : ICrudAppService<SelectStokDto, ListStokDto,
    StokListParameterDto, CreateStokDto, UpdateStokDto, CodeParameterDto>
{
}