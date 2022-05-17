using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Stoklar;

//[Authorize(OnMuhasebePermissions.Stok.Default)]
public class StokAppService : OnMuhasebeAppService, IStokAppService
{
    private readonly IStokRepository _stokRepository;
    private readonly StokManager _stokManager;

    public StokAppService(IStokRepository stokRepository, StokManager stokManager)
    {
        _stokRepository = stokRepository;
        _stokManager = stokManager;
    }
    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectStokDto> GetAsync(Guid id)
    {
        var entity = await _stokRepository.GetAsync(id, x => x.Id == id, x => x.Birim,
            x => x.OzelKod1, x => x.OzelKod2);

        return ObjectMapper.Map<Stok, SelectStokDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListStokDto>> GetListAsync(
        StokListParameterDto input)
    {
        var entities = await _stokRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod);

        var totalCount = await _stokRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListStokDto>(
            totalCount,
            ObjectMapper.Map<List<Stok>, List<ListStokDto>>(entities)
            );
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
    //[Authorize(OnMuhasebePermissions.Stok.Create)]
    public virtual async Task<SelectStokDto> CreateAsync(CreateStokDto input)
    {
        await _stokManager.CheckCreateAsync(input.Kod, input.BirimId, input.OzelKod1Id,
            input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateStokDto, Stok>(input);
        await _stokRepository.InsertAsync(entity);
        return ObjectMapper.Map<Stok, SelectStokDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    //[Authorize(OnMuhasebePermissions.Stok.Update)]
    public virtual async Task<SelectStokDto> UpdateAsync(Guid id, UpdateStokDto input)
    {
        var entity = await _stokRepository.GetAsync(id, x => x.Id == id);

        await _stokManager.CheckUpdateAsync(id, input.Kod, entity, input.BirimId,
            input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _stokRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Stok, SelectStokDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    //[Authorize(OnMuhasebePermissions.Stok.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _stokManager.CheckDeleteAsync(id);
        await _stokRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _stokRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}