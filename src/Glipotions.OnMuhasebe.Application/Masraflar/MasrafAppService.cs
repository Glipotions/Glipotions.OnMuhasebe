using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Masraflar;

public class MasrafAppService : OnMuhasebeAppService, IMasrafAppService
{
    private readonly IMasrafRepository _masrafRepository;
    private readonly MasrafManager _masrafManager;

    public MasrafAppService(IMasrafRepository masrafRepository, MasrafManager masrafManager)
    {
        _masrafRepository = masrafRepository;
        _masrafManager = masrafManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectMasrafDto> GetAsync(Guid id)
    {
        var entity = await _masrafRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Masraf, SelectMasrafDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListMasrafDto>> GetListAsync(MasrafListParameterDto input)
    {
        var entities = await _masrafRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod                         // orderby
            );

        var totalCount = await _masrafRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListMasrafDto>(totalCount,
            ObjectMapper.Map<List<Masraf>, List<ListMasrafDto>>(entities));
    }

    /// <Özet>
    /// UI'dan Create(Entity)Dto gelecek bunu Entity'e mapler ve o şekilde database e gönderir
    /// Sadece Domain katmanındaki Entityler Database'e gönderilebilir, DTO lar gönderilemez.
    /// Bu nedenle Mapleme yapıldı.
    /// 
    /// InsertAsync ile Databasede Create yapılmış oluyor.
    /// 
    /// Databaseden entity geliyor, 
    /// return kısmında ise bu entity'i tekrar mapleyerek Select(Entity)Dto olarak döndürüyor.
    public virtual async Task<SelectMasrafDto> CreateAsync(CreateMasrafDto input)
    {
        await _masrafManager.CheckCreateAsync(input.Kod, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateMasrafDto, Masraf>(input);
        await _masrafRepository.InsertAsync(entity);
        return ObjectMapper.Map<Masraf, SelectMasrafDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectMasrafDto> UpdateAsync(Guid id, UpdateMasrafDto input)
    {
        var entity = await _masrafRepository.GetAsync(id, x => x.Id == id);

        await _masrafManager.CheckUpdateAsync(id, input.Kod, entity, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _masrafRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Masraf, SelectMasrafDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        await _masrafManager.CheckDeleteAsync(id);
        await _masrafRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _masrafRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
