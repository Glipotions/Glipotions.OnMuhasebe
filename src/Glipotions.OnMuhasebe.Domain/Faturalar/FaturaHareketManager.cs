using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Faturalar;

public class FaturaHareketManager : DomainService
{
    private readonly IStokRepository _stokRepository;
    private readonly IHizmetRepository _hizmetRepository;
    private readonly IMasrafRepository _masrafRepository;
    private readonly IDepoRepository _depoRepository;

    public FaturaHareketManager(IStokRepository stokRepository,
        IHizmetRepository hizmetRepository, IMasrafRepository masrafRepository,
        IDepoRepository depoRepository)
    {
        _stokRepository = stokRepository;
        _hizmetRepository = hizmetRepository;
        _masrafRepository = masrafRepository;
        _depoRepository = depoRepository;
    }

    public async Task CheckCreateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId,
        Guid? depoId)
    {
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }

    public async Task CheckUpdateAsync(Guid? stokId, Guid? hizmetId, Guid? masrafId,
        Guid? depoId)
    {
        await _stokRepository.EntityAnyAsync(stokId, x => x.Id == stokId);
        await _hizmetRepository.EntityAnyAsync(hizmetId, x => x.Id == hizmetId);
        await _masrafRepository.EntityAnyAsync(masrafId, x => x.Id == masrafId);
        await _depoRepository.EntityAnyAsync(depoId, x => x.Id == depoId);
    }
}