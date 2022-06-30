namespace Glipotions.OnMuhasebe.Donemler;

[Authorize(OnMuhasebePermissions.Donem.Default)]
public class DonemAppService : OnMuhasebeAppService, IDonemAppService
{
    private readonly IDonemRepository _donemRepository;
    private readonly DonemManager _donemManager;

    public DonemAppService(IDonemRepository donemRepository, DonemManager donemManager)
    {
        _donemRepository = donemRepository;
        _donemManager = donemManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectDonemDto> GetAsync(Guid id)
    {
        var entity = await _donemRepository.GetAsync(id, x => x.Id == id);
        return ObjectMapper.Map<Donem, SelectDonemDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListDonemDto>> GetListAsync(DonemListParameterDto input)
    {
        var entities = await _donemRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,        // predicate
            x => x.Kod                         // orderby
            );

        var totalCount = await _donemRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListDonemDto>(totalCount,
            ObjectMapper.Map<List<Donem>, List<ListDonemDto>>(entities));
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
    [Authorize(OnMuhasebePermissions.Donem.Create)]
    public virtual async Task<SelectDonemDto> CreateAsync(CreateDonemDto input)
    {
        await _donemManager.CheckCreateAsync(input.Kod);

        var entity = ObjectMapper.Map<CreateDonemDto, Donem>(input);
        await _donemRepository.InsertAsync(entity);
        return ObjectMapper.Map<Donem, SelectDonemDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    [Authorize(OnMuhasebePermissions.Donem.Update)]
    public virtual async Task<SelectDonemDto> UpdateAsync(Guid id, UpdateDonemDto input)
    {
        var entity = await _donemRepository.GetAsync(id, x => x.Id == id);

        await _donemManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _donemRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<Donem, SelectDonemDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    [Authorize(OnMuhasebePermissions.Donem.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _donemManager.CheckDeleteAsync(id);
        await _donemRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _donemRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
