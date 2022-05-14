using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Depolar;

public class DepoManager : DomainService
{
    private readonly IDepoRepository _depoRepository;
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly ISubeRepository _subeRepository;

    public DepoManager(IDepoRepository depoRepository, IOzelKodRepository ozelKodRepository,
        ISubeRepository subeRepository)
    {
        _depoRepository = depoRepository;
        _ozelKodRepository = ozelKodRepository;
        _subeRepository = subeRepository;
    }

    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// <param name="kod"></param>          varsa tekrar eden kod hatası
    /// <param name="ozelKod1Id"></param>   yoksa böyle bir id yok hatası
    /// <param name="ozelKod2Id"></param>   yoksa böyle bir id yok hatası
    /// <returns></returns>
    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id, Guid? subeId)
    {
        await _subeRepository.EntityAnyAsync(subeId, x => x.Id == subeId);
        await _depoRepository.KodAnyAsync(kod, x => x.Kod == kod && x.SubeId == subeId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Depo);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Depo);
    }

    /// <Özet>
    /// Update işlemi yaparken hata kontrol etme yeri
    /// kodlar birbirine eşitse check etmeye gerek yok
    /// gelen kod ile entity.kod işlemi aynı ise hiç check etmeden direkt bu aşamayı geç.
    /// 
    /// ozelKod Idleri birbirinden farklı ise check et, değilse işlemi geç
    public async Task CheckUpdateAsync(Guid id, string kod, Depo entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _depoRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod &&
        x.SubeId == entity.SubeId, entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Depo, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Depo, entity.OzelKod2Id != ozelKod2Id);
    }

    /// <Özet>
    /// Silme işlemi yaparken kontrol eder sorun yoksa yapar varsa hata fırlatır.
    /// -
    /// entity kullanılmışsa iptal et, kullanılmamışsa izin ver.
    public async Task CheckDeleteAsync(Guid id)
    {
        await _depoRepository.RelationalEntityAnyAsync(
            x => x.FaturaHareketler.Any(y => y.DepoId == id));
    }
}