namespace Glipotions.OnMuhasebe.Hizmetler;

[Authorize(OnMuhasebePermissions.Hizmet.Default)]
public class HizmetAppService : OnMuhasebeAppService, IHizmetAppService
{
    private readonly IHizmetRepository _hizmetRepository;
    private readonly HizmetManager _hizmetManager;

    public HizmetAppService(IHizmetRepository hizmetRepository, HizmetManager hizmetManager)
    {
        _hizmetRepository = hizmetRepository;
        _hizmetManager = hizmetManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectHizmetDto> GetAsync(Guid id)
    {
        var entity = await _hizmetRepository.GetAsync(id, x => x.Id == id, x => x.Birim, x => x.OzelKod1, x => x.OzelKod2);
        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListHizmetDto>> GetListAsync(HizmetListParameterDto input)
    {
        var entities = await _hizmetRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod                         // orderby
            );

        var totalCount = await _hizmetRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListHizmetDto>(totalCount,
            ObjectMapper.Map<List<Hizmet>, List<ListHizmetDto>>(entities));
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
    [Authorize(OnMuhasebePermissions.Hizmet.Create)]
    public virtual async Task<SelectHizmetDto> CreateAsync(CreateHizmetDto input)
    {
        await _hizmetManager.CheckCreateAsync(input.Kod, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);

        var entity = ObjectMapper.Map<CreateHizmetDto, Hizmet>(input);
        await _hizmetRepository.InsertAsync(entity);
        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    [Authorize(OnMuhasebePermissions.Hizmet.Update)]
    public virtual async Task<SelectHizmetDto> UpdateAsync(Guid id, UpdateHizmetDto input)
    {
        var entity = await _hizmetRepository.GetAsync(id, x => x.Id == id);

        await _hizmetManager.CheckUpdateAsync(id, input.Kod, entity, input.BirimId, input.OzelKod1Id, input.OzelKod2Id);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _hizmetRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Hizmet, SelectHizmetDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    [Authorize(OnMuhasebePermissions.Hizmet.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _hizmetManager.CheckDeleteAsync(id);
        await _hizmetRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _hizmetRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
