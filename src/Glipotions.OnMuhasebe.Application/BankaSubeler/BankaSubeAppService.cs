using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.BankaSubeler;

public class BankaSubeAppService : OnMuhasebeAppService, IBankaSubeAppService
{
    private readonly IBankaSubeRepository _bankaSubeRepository;
    private readonly BankaSubeManager _bankaSubeManager;

    public BankaSubeAppService(IBankaSubeRepository bankaSubeRepository, BankaSubeManager bankaSubeManager)
    {
        _bankaSubeRepository = bankaSubeRepository;
        _bankaSubeManager = bankaSubeManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    /// x.Banka, x.OzelKod1, x.OzelKod2 --> Entitydeki Propertyler.
    public virtual async Task<SelectBankaSubeDto> GetAsync(Guid id)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, bs => bs.Id == id,
            x => x.Banka, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListBankaSubeDto>> GetListAsync(BankaSubeListParameterDto input)
    {
        var entities = await _bankaSubeRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.BankaId == input.BankaId && x.Durum == input.Durum,          // predicate
            x => x.Kod,                                                         // orderby
            x => x.Banka, x => x.OzelKod1, x => x.OzelKod2                      // include properties
            );

        var totalCount = await _bankaSubeRepository.CountAsync(x => x.BankaId == input.BankaId && x.Durum == input.Durum);

        return new PagedResultDto<ListBankaSubeDto>(totalCount,
            ObjectMapper.Map<List<BankaSube>, List<ListBankaSubeDto>>(entities));
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
    public virtual async Task<SelectBankaSubeDto> CreateAsync(CreateBankaSubeDto input)
    {
        await _bankaSubeManager.CheckCreateAsync(input.Kod, input.BankaId, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateBankaSubeDto, BankaSube>(input);
        await _bankaSubeRepository.InsertAsync(entity);
        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectBankaSubeDto> UpdateAsync(Guid id, UpdateBankaSubeDto input)
    {
        var entity = await _bankaSubeRepository.GetAsync(id, x => x.Id == id);

        await _bankaSubeManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaSubeRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<BankaSube, SelectBankaSubeDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaSubeManager.CheckDeleteAsync(id);

        await _bankaSubeRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(BankaSubeCodeParameterDto input)
    {
        return await _bankaSubeRepository.GetCodeAsync(x => x.Kod,
            x => x.BankaId == input.BankaId && x.Durum == input.Durum);
    }

}
