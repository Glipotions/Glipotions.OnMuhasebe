using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Birimler;

public class BirimManager : DomainService
{
    private readonly IBirimRepository _birimRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BirimManager(IBirimRepository birimRepository, IOzelKodRepository ozelKodRepository)
    {
        _birimRepository = birimRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// <param name="kod"></param>          varsa tekrar eden kod hatası
    /// <param name="ozelKod1Id"></param>   yoksa böyle bir id yok hatası
    /// <param name="ozelKod2Id"></param>   yoksa böyle bir id yok hatası
    /// <returns></returns>
    public async Task CheckCreateAsync(string kod, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _birimRepository.KodAnyAsync(kod, x => x.Kod == kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Birim);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Birim);
    }

    /// <Özet>
    /// Update işlemi yaparken hata kontrol etme yeri
    /// kodlar birbirine eşitse check etmeye gerek yok
    /// gelen kod ile entity.kod işlemi aynı ise hiç check etmeden direkt bu aşamayı geç.
    /// 
    /// ozelKod Idleri birbirinden farklı ise check et, değilse işlemi geç
    public async Task CheckUpdateAsync(Guid id, string kod, Birim entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _birimRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.Birim, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.Birim, entity.OzelKod2Id != ozelKod2Id);
    }

    /// <Özet>
    /// Silme işlemi yaparken kontrol eder sorun yoksa yapar varsa hata fırlatır.
    /// -
    /// entity kullanılmışsa iptal et, kullanılmamışsa izin ver.
    public async Task CheckDeleteAsync(Guid id)
    {
        await _birimRepository.RelationalEntityAnyAsync(
            x => x.Hizmetler.Any(y => y.BirimId == id) ||
                 x.Masraflar.Any(y => y.BirimId == id) ||
                 x.Stoklar.Any(y => y.BirimId == id));
    }
}