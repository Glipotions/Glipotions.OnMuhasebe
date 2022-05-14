using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Cariler;

public class CariAppService : OnMuhasebeAppService, ICariAppService
{
    private readonly ICariRepository _cariRepository;
    private readonly CariManager _cariManager;

    public CariAppService(ICariRepository cariRepository, CariManager cariManager)
    {
        _cariRepository = cariRepository;
        _cariManager = cariManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectCariDto> GetAsync(Guid id)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListCariDto>> GetListAsync(CariListParameterDto input)
    {
        var entities = await _cariRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod,                         // orderby
            x => x.OzelKod1, x => x.OzelKod2, x => x.Faturalar, x => x.Makbuzlar    // include properties
            );

        var totalCount = await _cariRepository.CountAsync(x => x.Durum == input.Durum);
        var mappedDtos=ObjectMapper.Map<List<Cari>, List< ListCariDto>>(entities);

        mappedDtos.ForEach(x =>
        {
            x.Borc = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Alis).Sum(y => y.NetTutar);

            x.Borc += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Tahsilat)
                .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
                 y.BankaToplam);

            x.Alacak = x.Faturalar.Where(y => y.FaturaTuru == FaturaTuru.Satis).Sum(y => y.NetTutar);

            x.Alacak += x.Makbuzlar.Where(y => y.MakbuzTuru == MakbuzTuru.Odeme)
                .Sum(y => y.CekToplam + y.SenetToplam + y.PosToplam + y.NakitToplam +
                 y.BankaToplam);
        });

        return new PagedResultDto<ListCariDto>(totalCount, mappedDtos);
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
    public virtual async Task<SelectCariDto> CreateAsync(CreateCariDto input)
    {
        await _cariManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateCariDto, Cari>(input);
        await _cariRepository.InsertAsync(entity);
        return ObjectMapper.Map<Cari, SelectCariDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectCariDto> UpdateAsync(Guid id, UpdateCariDto input)
    {
        var entity = await _cariRepository.GetAsync(id, x => x.Id == id);

        await _cariManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _cariRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Cari, SelectCariDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        await _cariManager.CheckDeleteAsync(id);
        await _cariRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _cariRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
