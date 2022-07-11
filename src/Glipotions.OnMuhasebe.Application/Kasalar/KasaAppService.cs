namespace Glipotions.OnMuhasebe.Kasalar;

[Authorize(OnMuhasebePermissions.Kasa.Default)]
public class KasaAppService : OnMuhasebeAppService, IKasaAppService
{
    private readonly IKasaRepository _kasaRepository;
    private readonly KasaManager _kasaManager;

    public KasaAppService(IKasaRepository kasaRepository, KasaManager kasaManager)
    {
        _kasaRepository = kasaRepository;
        _kasaManager = kasaManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectKasaDto> GetAsync(Guid id)
    {
        var entity = await _kasaRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Kasa, SelectKasaDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListKasaDto>> GetListAsync(KasaListParameterDto input)
    {
        var entities = await _kasaRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.SubeId == input.SubeId &&
            x.Durum == input.Durum,        // predicate
            x => x.Kod,                         // orderby
            x => x.OzelKod1, x => x.OzelKod2, x=>x.MakbuzHareketler    // include properties
            );

        var totalCount = await _kasaRepository.CountAsync(x => x.SubeId == input.SubeId && 
            x.Durum == input.Durum);

        return new PagedResultDto<ListKasaDto>(totalCount,
            ObjectMapper.Map<List<Kasa>, List<ListKasaDto>>(entities));
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
    [Authorize(OnMuhasebePermissions.Kasa.Create)]
    public virtual async Task<SelectKasaDto> CreateAsync(CreateKasaDto input)
    {
        await _kasaManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateKasaDto, Kasa>(input);
        await _kasaRepository.InsertAsync(entity);
        return ObjectMapper.Map<Kasa, SelectKasaDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    [Authorize(OnMuhasebePermissions.Kasa.Update)]
    public virtual async Task<SelectKasaDto> UpdateAsync(Guid id, UpdateKasaDto input)
    {
        var entity = await _kasaRepository.GetAsync(id, x => x.Id == id);

        await _kasaManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _kasaRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Kasa, SelectKasaDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    [Authorize(OnMuhasebePermissions.Kasa.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _kasaManager.CheckDeleteAsync(id);
        await _kasaRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(KasaCodeParameterDto input)
    {
        return await _kasaRepository.GetCodeAsync(x => x.Kod,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum);
    }
}
