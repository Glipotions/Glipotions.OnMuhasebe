using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public class BankaHesapManager : DomainService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;

    public BankaHesapManager(IBankaHesapRepository bankaHesapRepository,
        IBankaSubeRepository bankaSubeRepository, IOzelKodRepository ozelKodRepository,
        ISubeRepository subeRepository)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaSubeRepository = bankaSubeRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
    }

    public async Task CheckCreateAsync(string kod, Guid? bankaSubeId, Guid? ozelKod1Id,
        Guid? ozelKod2Id, Guid? subeId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _bankaHesapRepository.KodAnyAsync(kod, x => x.Kod == kod && x.SubeId == subeId);
        await _bankaSubeRepository.EntityAnyAsync(bankaSubeId, x => x.Id == bankaSubeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaHesap);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaHesap);
    }

    public async Task CheckUpdateAsync(Guid id, string kod, BankaHesap entity,
        Guid? bankaSubeId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaHesapRepository.KodAnyAsync(kod,
            x => x.Id != id && x.Kod == kod && x.SubeId == entity.SubeId,
            entity.Kod != kod);

        await _bankaSubeRepository.EntityAnyAsync(bankaSubeId, x => x.Id == bankaSubeId,
            entity.BankaSubeId != bankaSubeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaHesap, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaHesap, entity.OzelKod2Id != ozelKod2Id);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaHesapRepository.RelationalEntityAnyAsync(
            x => x.Makbuzlar.Any(y => y.BankaHesapId == id) ||
                 x.MakbuzHareketler.Any(y => y.BankaHesapId == id));
    }
}