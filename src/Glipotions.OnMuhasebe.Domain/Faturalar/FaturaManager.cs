using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Faturalar;

public class FaturaManager : DomainService
{
    private readonly IFaturaRepository _fauraRepository;
    private readonly ICariRepository _cariRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;
    private readonly IDonemRepository _donemRepository;

    public FaturaManager(IFaturaRepository fauraRepository, ICariRepository cariRepository,
        IOzelKodRepository ozelKodRepository, ISubeRepository subeRepository,
        IDonemRepository donemRepository)
    {
        _fauraRepository = fauraRepository;
        _cariRepository = cariRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
        _donemRepository = donemRepository;
    }
    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// <param name="faturaNo"></param>     varsa tekrar eden kod hatası
    /// <param name="ozelKod1Id"></param>   yoksa böyle bir id yok hatası
    /// <param name="ozelKod2Id"></param>   yoksa böyle bir id yok hatası
    /// <param name="subeId"></param>       yoksa böyle bir subeid yok hatası
    /// <param name="donemId"></param>      yoksa böyle bir donemId yok hatası
    /// <param name="cariId"></param>       yoksa böyle bir cariId yok hatası
    /// <returns></returns>
    public async Task CheckCreateAsync(string faturaNo, Guid? cariId, Guid? ozelKod1Id,
        Guid? ozelKod2Id, Guid? subeId, Guid? donemId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _donemRepository.EntityAnyAsync(donemId, x => x.Id == donemId);

        await _fauraRepository.KodAnyAsync(faturaNo, x => x.FaturaNo == faturaNo &&
        x.SubeId == subeId && x.DonemId == donemId);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Fatura);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Fatura);
        
    }

    public async Task CheckUpdateAsync(Guid id, string faturaNo, Fatura entity, Guid? cariId,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _fauraRepository.KodAnyAsync(faturaNo, x => x.Id != id &&
        x.FaturaNo == faturaNo && x.SubeId == entity.SubeId && x.DonemId == entity.DonemId,
        entity.FaturaNo != faturaNo);

        await _cariRepository.EntityAnyAsync(cariId, x => x.Id == cariId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Fatura, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Fatura, entity.OzelKod2Id != ozelKod2Id);
    }
}