using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.Subeler;

public class SubeManager : DomainService
{
    private readonly ISubeRepository _subeRepository;

    public SubeManager(ISubeRepository subeRepository)
    {
        _subeRepository = subeRepository;
    }
    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// <param name="kod"></param>          varsa tekrar eden kod hatası
    /// <returns></returns>
    public async Task CheckCreateAsync(string kod)
    {
        await _subeRepository.KodAnyAsync(kod, x => x.Kod == kod);
    }
    /// <Özet>
    /// Update işlemi yaparken hata kontrol etme yeri
    /// kodlar birbirine eşitse check etmeye gerek yok
    /// gelen kod ile entity.kod işlemi aynı ise hiç check etmeden direkt bu aşamayı geç.
    public async Task CheckUpdateAsync(Guid id, string kod, Sube entity)
    {
        await _subeRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod,
            entity.Kod != kod);
    }
    /// <Özet>
    /// Silme işlemi yaparken kontrol eder sorun yoksa yapar varsa hata fırlatır.
    /// -
    /// entity kullanılmışsa iptal et, kullanılmamışsa izin ver.
    public async Task CheckDeleteAsync(Guid id)
    {
        await _subeRepository.RelationalEntityAnyAsync(
            x => x.BankaHesaplar.Any(y => y.SubeId == id) ||
                 x.Depolar.Any(y => y.SubeId == id) ||
                 x.Faturalar.Any(y => y.SubeId == id) ||
                 x.Kasalar.Any(y => y.SubeId == id) ||
                 x.Makbuzlar.Any(y => y.SubeId == id) ||
                 x.FirmaParemetreler.Any(y => y.SubeId == id));
    }
}