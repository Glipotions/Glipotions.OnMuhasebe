using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Parametreler;

public class FirmaParametreManager : DomainService
{
    private readonly ISubeRepository _subeRepository;
    private readonly IDonemRepository _donemRepository;
    private readonly IDepoRepository _depoRepository;

    public FirmaParametreManager(ISubeRepository subeRepository,
        IDonemRepository donemRepository, IDepoRepository depoRepository)
    {
        _subeRepository = subeRepository;
        _donemRepository = donemRepository;
        _depoRepository = depoRepository;
    }

    public async Task CheckCreateAsync(Guid? subeId, Guid? donemId, Guid? depoId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }

    public async Task CheckUpdateAsync(Guid? subeId, Guid? donemId, Guid? depoId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }
}