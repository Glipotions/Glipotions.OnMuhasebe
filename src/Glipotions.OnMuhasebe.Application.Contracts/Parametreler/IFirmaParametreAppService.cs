using System;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Services;

namespace Glipotions.OnMuhasebe.Parametreler;

public interface IFirmaParametreAppService : ICrudAppService<SelectFirmaParametreDto,
    SelectFirmaParametreDto, FirmaParametreListParameterDto, CreateFirmaParametreDto,
    UpdateFirmaParametreDto>
{
    Task<bool> UserAnyAsync(Guid userId);
}