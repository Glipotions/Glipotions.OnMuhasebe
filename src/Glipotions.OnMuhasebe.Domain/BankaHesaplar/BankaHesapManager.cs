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

    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// 
    /// <param name="kod"></param>          varsa tekrar eden kod hatası
    /// <param name="bankaSubeId"></param>      böyle bir bankaSubeId var mı kontrolü için gerekir
    /// <param name="ozelKod1Id"></param>   böyle bir ozelKod1Id var mı kontrolü için gerekir
    /// <param name="ozelKod2Id"></param>   böyle bir ozelKod2Id var mı kontrolü için gerekir
    /// EntityAnyAsync ile database de var mı kontrolü yapılır.
    /// <returns></returns>
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

    /// <Özet>
    /// Update işlemi yaparken hata kontrol etme yeri
    /// kodlar birbirine eşitse check etmeye gerek yok
    /// gelen kod ile entity.kod işlemi aynı ise hiç check etmeden direkt bu aşamayı geç.
    /// 
    /// ozelKod Idleri birbirinden farklı ise check et, değilse işlemi geç
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
    /// <Özet>
    /// Silme işlemi yaparken kontrol eder sorun yoksa yapar varsa hata fırlatır.
    /// -
    /// entity kullanılmışsa iptal et, kullanılmamışsa izin ver.
    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaHesapRepository.RelationalEntityAnyAsync(
            x => x.Makbuzlar.Any(y => y.BankaHesapId == id) ||
                 x.MakbuzHareketler.Any(y => y.BankaHesapId == id));
    }
}