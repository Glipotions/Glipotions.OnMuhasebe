namespace Glipotions.OnMuhasebe.Depolar;

[Authorize(OnMuhasebePermissions.Depo.Default)]
public class DepoAppService : OnMuhasebeAppService, IDepoAppService
{
    private readonly IDepoRepository _depoRepository;
    private readonly DepoManager _depoManager;

    public DepoAppService(IDepoRepository depoRepository, DepoManager depoManager)
    {
        _depoRepository = depoRepository;
        _depoManager = depoManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectDepoDto> GetAsync(Guid id)
    {
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListDepoDto>> GetListAsync(DepoListParameterDto input)
    {
        var entities = await _depoRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.SubeId == input.SubeId && x.Durum == input.Durum,        // predicate
            x => x.Kod,                         // orderby
            x => x.OzelKod1, x => x.OzelKod2    // include properties
            );

        var totalCount = await _depoRepository.CountAsync(x => x.SubeId == input.SubeId && 
            x.Durum == input.Durum);

        return new PagedResultDto<ListDepoDto>(totalCount,
            ObjectMapper.Map<List<Depo>, List<ListDepoDto>>(entities));
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
    [Authorize(OnMuhasebePermissions.Depo.Create)]
    public virtual async Task<SelectDepoDto> CreateAsync(CreateDepoDto input)
    {
        await _depoManager.CheckCreateAsync(input.Kod, input.OzelKod1Id, input.OzelKod2Id, input.SubeId);

        var entity = ObjectMapper.Map<CreateDepoDto, Depo>(input);
        await _depoRepository.InsertAsync(entity);
        return ObjectMapper.Map<Depo, SelectDepoDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    [Authorize(OnMuhasebePermissions.Depo.Update)]
    public virtual async Task<SelectDepoDto> UpdateAsync(Guid id, UpdateDepoDto input)
    {
        var entity = await _depoRepository.GetAsync(id, x => x.Id == id);

        await _depoManager.CheckUpdateAsync(id, input.Kod, entity, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _depoRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Depo, SelectDepoDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    [Authorize(OnMuhasebePermissions.Depo.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _depoManager.CheckDeleteAsync(id);
        await _depoRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(DepoCodeParameterDto input)
    {
        return await _depoRepository.GetCodeAsync(x => x.Kod, x => x.SubeId == input.SubeId && 
            x.Durum == input.Durum);
    }
}
