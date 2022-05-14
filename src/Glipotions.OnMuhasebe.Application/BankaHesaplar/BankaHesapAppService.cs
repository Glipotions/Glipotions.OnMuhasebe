using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.BankaHesaplar;

public class BankaHesapAppService : OnMuhasebeAppService, IBankaHesapAppService
{
    private readonly IBankaHesapRepository _bankaHesapRepository;
    private readonly BankaHesapManager _bankaHesapManager;

    public BankaHesapAppService(IBankaHesapRepository bankaHesapRepository, BankaHesapManager bankaHesapManager)
    {
        _bankaHesapRepository = bankaHesapRepository;
        _bankaHesapManager = bankaHesapManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    /// x.Banka, x.BankaSube, x.OzelKod1, x.OzelKod2 --> Entitydeki Propertyler.
    /// mappedDto.HesapTuruAdi ile Localize işlemi yapılarak BankaHesapTuru enum ına göre hesap türü adı çekilir.
    public virtual async Task<SelectBankaHesapDto> GetAsync(Guid id)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id,
            x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2);
        var mappedDto = ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
        mappedDto.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)mappedDto.HesapTuru}"];

        return mappedDto;
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görevi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// mappedDtos ta foreach işlemi ile HesapTuruAdi ve Borc, Alacak değerleri doldurulur
    /// 
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListBankaHesapDto>> GetListAsync(BankaHesapListParameterDto input)
    {
        var entities = await _bankaHesapRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount, x => input.HesapTuru == null ? x.SubeId == input.SubeId &&
            x.Durum == input.Durum : x.HesapTuru == input.HesapTuru && x.SubeId == input.SubeId &&
            x.Durum == input.Durum,                                                                 // predicate
            x => x.Kod,                                                                             // orderby
            x => x.BankaSube, x => x.BankaSube.Banka, x => x.OzelKod1, x => x.OzelKod2,             // include properties
            x => x.MakbuzHareketler);

        var totalCount = await _bankaHesapRepository.CountAsync(x => input.HesapTuru == null ? x.SubeId == input.SubeId &&
            x.Durum == input.Durum : x.HesapTuru == input.HesapTuru && x.SubeId == input.SubeId &&
            x.Durum == input.Durum);

        var mappedDtos = ObjectMapper.Map<List<BankaHesap>, List<ListBankaHesapDto>>(entities);
        mappedDtos.ForEach(x =>
        {
            x.HesapTuruAdi = L[$"Enum:BankaHesapTuru:{(byte)x.HesapTuru}"];
            x.Borc = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi ||
                     y.OdemeTuru == OdemeTuru.Pos && y.BelgeDurumu == BelgeDurumu.Portfoyde)
                      .Sum(y => y.Tutar);

            x.Alacak = x.MakbuzHareketler.Where(y => y.BelgeDurumu == BelgeDurumu.TahsilEdildi)
                        .Sum(y => y.Tutar);

        });

        return new PagedResultDto<ListBankaHesapDto>(totalCount,
            ObjectMapper.Map<List<BankaHesap>, List<ListBankaHesapDto>>(entities));
    }
    /// <Özet>
    /// CheckCreateAsync ile Manager sınıfından database kontrolü yapılır.
    /// 
    /// UI'dan Create(Entity)Dto gelecek bunu Entity'e mapler ve o şekilde database e gönderir
    /// Sadece Domain katmanındaki Entityler Database'e gönderilebilir, DTO lar gönderilemez.
    /// Bu nedenle Mapleme yapıldı.
    /// 
    /// InsertAsync ile Databasede Create yapılmış oluyor.
    /// 
    /// Databaseden entity geliyor, 
    /// return kısmında ise bu entity'i tekrar mapleyerek Select(Entity)Dto olarak döndürüyor.
    public virtual async Task<SelectBankaHesapDto> CreateAsync(CreateBankaHesapDto input)
    {
        await _bankaHesapManager.CheckCreateAsync(input.Kod, input.BankaSubeId, input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateBankaHesapDto, BankaHesap>(input);
        await _bankaHesapRepository.InsertAsync(entity);
        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı ile entity üretmeye gerek olmadığından yapılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectBankaHesapDto> UpdateAsync(Guid id, UpdateBankaHesapDto input)
    {
        var entity = await _bankaHesapRepository.GetAsync(id, x => x.Id == id);

        await _bankaHesapManager.CheckUpdateAsync(id, input.Kod, entity, input.BankaSubeId, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaHesapRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<BankaHesap, SelectBankaHesapDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaHesapManager.CheckDeleteAsync(id);

        await _bankaHesapRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(BankaHesapCodeParameterDto input)
    {
        return await _bankaHesapRepository.GetCodeAsync(x => x.Kod,
            x => x.BankaSubeId == input.SubeId && x.Durum == input.Durum);
    }
}
