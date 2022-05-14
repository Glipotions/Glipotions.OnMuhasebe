using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Birimler;

public class BirimAppService : OnMuhasebeAppService, IBirimAppService
{
    private readonly IBirimRepository _birimRepository;
    private readonly BirimManager _birimManager;

    public BirimAppService(IBirimRepository birimRepository, BirimManager birimManager)
    {
        _birimRepository = birimRepository;
        _birimManager = birimManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectBirimDto> GetAsync(Guid id)
    {
        var entity = await _birimRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Birim, SelectBirimDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListBirimDto>> GetListAsync(BirimListParameterDto input)
    {
        var entities = await _birimRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod,                         // orderby
            x => x.OzelKod1, x => x.OzelKod2    // include properties
            );

        var totalCount = await _birimRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListBirimDto>(totalCount,
            ObjectMapper.Map<List<Birim>, List<ListBirimDto>>(entities));
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
    public virtual async Task<SelectBirimDto> CreateAsync(CreateBirimDto input)
    {
        await _birimManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateBirimDto, Birim>(input);
        await _birimRepository.InsertAsync(entity);
        return ObjectMapper.Map<Birim, SelectBirimDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectBirimDto> UpdateAsync(Guid id, UpdateBirimDto input)
    {
        var entity = await _birimRepository.GetAsync(id, x => x.Id == id);

        await _birimManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _birimRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Birim, SelectBirimDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        await _birimManager.CheckDeleteAsync(id);
        await _birimRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _birimRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
