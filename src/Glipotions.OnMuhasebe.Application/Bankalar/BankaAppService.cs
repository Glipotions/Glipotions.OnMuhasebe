﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Bankalar;

public class BankaAppService : OnMuhasebeAppService, IBankaAppService
{
    private readonly IBankaRepository _bankaRepository;

    /// <Özet>
    /// <param name="bankaRepository"></param> Yukarıda Oluşturduktan Sonra Generate Constructor yaptık
    public BankaAppService(IBankaRepository bankaRepository)
    {
        _bankaRepository = bankaRepository;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectBankaDto> GetAsync(Guid id)
    {
        var entity = await _bankaRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Banka, SelectBankaDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListBankaDto>> GetListAsync(BankaListParameterDto input)
    {
        var entities = await _bankaRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod,                         // orderby
            x => x.OzelKod1, x => x.OzelKod2    // include properties
            );

        var totalCount = await _bankaRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListBankaDto>(totalCount,
            ObjectMapper.Map<List<Banka>, List<ListBankaDto>>(entities));
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
    public virtual async Task<SelectBankaDto> CreateAsync(CreateBankaDto input)
    {


        var entity = ObjectMapper.Map<CreateBankaDto, Banka>(input);
        await _bankaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Banka, SelectBankaDto>(entity);
    }

    public virtual async Task<SelectBankaDto> UpdateAsync(Guid id, UpdateBankaDto input)
    {
        var entity = await _bankaRepository.GetAsync(id, x => x.Id == id);
        var mappedEntity = ObjectMapper.Map(input, entity);
        await _bankaRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Banka, SelectBankaDto>(mappedEntity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await _bankaRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _bankaRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
