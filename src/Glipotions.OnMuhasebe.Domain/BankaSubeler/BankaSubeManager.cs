using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Extensions;
using Volo.Abp.Domain.Services;

namespace Glipotions.OnMuhasebe.BankaSubeler;

public class BankaSubeManager : DomainService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly IBankaRepository _bankaRepository;
    private readonly IOzelKodRepository _ozelKodRepository;

    public BankaSubeManager(IBankaSubeRepository bankaSubeRepository, IBankaRepository bankaRepository,
        IOzelKodRepository ozelKodRepository)
    {
        _bankaSubeRepository = bankaSubeRepository;
        _bankaRepository = bankaRepository;
        _ozelKodRepository = ozelKodRepository;
    }

    /// <Özet>
    /// Create işlemi yaparken gelen id ve kod alanı ile ilgili hata var mı yok mu
    /// diye kontrol eder. hata varsa hata fırlatır.
    /// 
    /// <param name="kod"></param>          varsa tekrar eden kod hatası
    /// <param name="bankaId"></param>      böyle bir bankaId var mı kontrolü için gerekir
    /// <param name="ozelKod1Id"></param>   böyle bir ozelKod1Id var mı kontrolü için gerekir
    /// <param name="ozelKod2Id"></param>   böyle bir ozelKod2Id var mı kontrolü için gerekir
    /// EntityAnyAsync ile database de var mı kontrolü yapılır.
    /// <returns></returns>
    public async Task CheckCreateAsync(string kod, Guid? bankaId, Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaRepository.EntityAnyAsync(bankaId, x => x.Id == bankaId);

        await _bankaSubeRepository.KodAnyAsync(kod, x => x.Kod == kod && x.BankaId == bankaId);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaSube);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaSube);
    }

    /// <Özet>
    /// Update işlemi yaparken hata kontrol etme yeri
    /// kodlar birbirine eşitse check etmeye gerek yok
    /// gelen kod ile entity.kod işlemi aynı ise hiç check etmeden direkt bu aşamayı geç.
    /// 
    /// ozelKod Idleri birbirinden farklı ise check et, değilse işlemi geç
    public async Task CheckUpdateAsync(Guid id, string kod, BankaSube entity,
        Guid? ozelKod1Id, Guid? ozelKod2Id)
    {
        await _bankaSubeRepository.KodAnyAsync(kod, x => x.Id != id && x.Kod == kod && x.BankaId == entity.BankaId,
            entity.Kod != kod);

        await _ozelKodRepository.EntityAnyAsync(ozelKod1Id, OzelKodTuru.OzelKod1,
            KartTuru.BankaSube, entity.OzelKod1Id != ozelKod1Id);

        await _ozelKodRepository.EntityAnyAsync(ozelKod2Id, OzelKodTuru.OzelKod2,
            KartTuru.BankaSube, entity.OzelKod2Id != ozelKod2Id);
    }

    /// <Özet>
    /// Silme işlemi yaparken kontrol eder sorun yoksa yapar varsa hata fırlatır.
    /// -
    /// entity kullanılmışsa iptal et, kullanılmamışsa izin ver.
    public async Task CheckDeleteAsync(Guid id)
    {
        await _bankaSubeRepository.RelationalEntityAnyAsync(
            x => x.BankaHesaplar.Any(y => y.BankaSubeId == id) ||
                 x.MakbuzHareketler.Any(y => y.CekBankaSubeId == id));
    }
}
