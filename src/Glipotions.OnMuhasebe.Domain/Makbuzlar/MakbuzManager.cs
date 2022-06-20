using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Makbuzlar;

public class MakbuzManager : DomainService
{
    private readonly IMakbuzRepository _makbuzRepository;
    private readonly ICariRepository _cariRepository;
    private readonly IKasaRepository _kasaRepository;
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;
    private readonly IDonemRepository _donemRepository;

    public MakbuzManager(IMakbuzRepository makbuzRepository, ICariRepository cariRepository,
        IKasaRepository kasaRepository, IBankaHesapRepository bankaHesapRepository,
        IOzelKodRepository ozelKodRepository, ISubeRepository subeRepository,
        IDonemRepository donemRepository)
    {
        _makbuzRepository = makbuzRepository;
        _cariRepository = cariRepository;
        _kasaRepository = kasaRepository;
        _bankaHesapRepository = bankaHesapRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
        _donemRepository = donemRepository;
    }

    public async Task CheckCreateAsync(string makbuzNo, MakbuzTuru makbuzTuru, Guid? cariId,
        Guid? kasaId, Guid? bankaHesapId, Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? subeId,
        Guid? donemId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);

        await _makbuzRepository.KodAnyAsync(makbuzNo, x => x.MakbuzNo == makbuzNo &&
            x.MakbuzTuru == makbuzTuru && x.SubeId == subeId && x.DonemId == donemId);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);
        await _kasaRepository.EntityAnyAsync(kasaId, x => x.Id == kasaId);
        await _bankaHesapRepository.EntityAnyAsync(bankaHesapId, x => x.Id == bankaHesapId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Makbuz);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Makbuz);
    }

    public async Task CheckUpdateAsync(Guid id, string makbuzNo, Makbuz entity,
        Guid? cariId, Guid? kasaId, Guid? bankaHesapId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _makbuzRepository.KodAnyAsync(makbuzNo, x => x.Id != id &&
        x.MakbuzNo == makbuzNo && x.SubeId == entity.SubeId && x.DonemId == entity.DonemId,
        entity.MakbuzNo != makbuzNo);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);
        await _kasaRepository.EntityAnyAsync(kasaId, x => x.Id == kasaId);
        await _bankaHesapRepository.EntityAnyAsync(bankaHesapId, x => x.Id == bankaHesapId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Makbuz, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Makbuz, entity.OzelKod2Id != ozelKod2Id);
    }
}