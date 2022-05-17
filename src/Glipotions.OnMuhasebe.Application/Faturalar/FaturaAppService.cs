using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.CommonDtos;
using Glipotions.OnMuhasebe.FaturaHareketler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Faturalar;

public class FaturaAppService : OnMuhasebeAppService, IFaturaAppService
{
    private readonly IFaturaRepository _faturaRepository;
    private readonly FaturaManager _faturaManager;
    private readonly FaturaHareketManager _faturaHareketManager;

    public FaturaAppService(IFaturaRepository faturaRepository, FaturaManager faturaManager,
        FaturaHareketManager faturaHareketManager)
    {
        _faturaRepository = faturaRepository;
        _faturaManager = faturaManager;
        _faturaHareketManager = faturaHareketManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    /// include property eklenmek, EFCoreRepository de bu işlem yapılır.
    /// Fatura Hareketler de HareketTuru nü burda doldurmak gerekir.
    public virtual async Task<SelectFaturaDto> GetAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id);

        var mappedDto = ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
        mappedDto.FaturaHareketler.ForEach(x =>
        {
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });

        return mappedDto;
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListFaturaDto>> GetListAsync(FaturaListParameterDto input)
    {
        var entities = await _faturaRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum,  
            x => x.FaturaNo,
            x => x.Cari, x => x.OzelKod1, x => x.OzelKod2);

        var totalCount = await _faturaRepository.CountAsync(
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);

        return new PagedResultDto<ListFaturaDto>(
            totalCount,
            ObjectMapper.Map<List<Fatura>, List<ListFaturaDto>>(entities)
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
    public virtual async Task<SelectFaturaDto> CreateAsync(CreateFaturaDto input)
    {
        await _faturaManager.CheckCreateAsync(input.FaturaNo, input.CariId, input.OzelKod1Id,
            input.OzelKod2Id, input.SubeId, input.DonemId);

        foreach (var faturaHareket in input.FaturaHareketler)
        {
            await _faturaHareketManager.CheckCreateAsync(faturaHareket.StokId,
                faturaHareket.HizmetId, faturaHareket.MasrafId, faturaHareket.DepoId);
        }

        var entity = ObjectMapper.Map<CreateFaturaDto, Fatura>(input);
        await _faturaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// faturaHareketDto döngüsünde bir faturaHareket entitysi elde edilir..
    /// eğer null ise fatura güncelleniyor ancak yeni bir hareket eklenecek demektir..
    /// yeni eklenecek hareketi .Add komutu ile eklenir daha sonra elde edilen entity maplenir.
    /// 
    /// deletedEntities ile faturada silinen hareketleri tutar
    /// RemoveAll ile silinir. Databaseden tam anlamıyla silinmiyor isDeleted=true durumuna getirilir.
    /// 
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectFaturaDto> UpdateAsync(Guid id, UpdateFaturaDto input)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id,
            x => x.FaturaHareketler);

        await _faturaManager.CheckUpdateAsync(id, input.FaturaNo, entity, input.CariId,
            input.OzelKod1Id, input.OzelKod2Id);

        foreach (var faturaHareketDto in input.FaturaHareketler)
        {
            await _faturaHareketManager.CheckUpdateAsync(faturaHareketDto.StokId,
                faturaHareketDto.HizmetId, faturaHareketDto.MasrafId, faturaHareketDto.DepoId);

            var faturaHareket = entity.FaturaHareketler.FirstOrDefault(
                x => x.Id == faturaHareketDto.Id);

            if (faturaHareket == null)
            {
                entity.FaturaHareketler.Add(
                    ObjectMapper.Map<FaturaHareketDto, FaturaHareket>(faturaHareketDto));
                continue;
            }

            ObjectMapper.Map(faturaHareketDto, faturaHareket);
        }

        var deletedEntities = entity.FaturaHareketler.Where(
            x => input.FaturaHareketler.Select(y => y.Id).ToList().IndexOf(x.Id) == -1);

        entity.FaturaHareketler.RemoveAll(deletedEntities);

        ObjectMapper.Map(input, entity);
        await _faturaRepository.UpdateAsync(entity);
        return ObjectMapper.Map<Fatura, SelectFaturaDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _faturaRepository.GetAsync(id, x => x.Id == id,
            x => x.FaturaHareketler);

        await _faturaRepository.DeleteAsync(entity);
        entity.FaturaHareketler.RemoveAll(entity.FaturaHareketler);
    }

    public virtual async Task<string> GetCodeAsync(FaturaNoParameterDto input)
    {
        return await _faturaRepository.GetCodeAsync(x => x.FaturaNo,
            x => x.FaturaTuru == input.FaturaTuru && x.SubeId == input.SubeId &&
                 x.DonemId == input.DonemId && x.Durum == input.Durum);
    }
}
