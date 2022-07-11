namespace Glipotions.OnMuhasebe.OzelKodlar;

[Authorize(OnMuhasebePermissions.OzelKod.Default)]
public class OzelKodAppService : OnMuhasebeAppService, IOzelKodAppService
{
    private readonly IOzelKodRepository _ozelKodRepository;
    private readonly OzelKodManager _ozelKodManager;

    public OzelKodAppService(IOzelKodRepository ozelKodRepository, OzelKodManager ozelKodManager)
    {
        _ozelKodRepository = ozelKodRepository;
        _ozelKodManager = ozelKodManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectOzelKodDto> GetAsync(Guid id)
    {
        var entity = await _ozelKodRepository.GetAsync(id, x => x.Id == id);
        return ObjectMapper.Map<OzelKod, SelectOzelKodDto>(entity);
    }
    /// <Özet>
    /// Listeyi Çağırır
    /// predicate --> filtre görebi görür
    /// <param name="input"></param>
    /// include propertyler girilir.
    /// return olarak Entity'i List(Entity)Dto ya mapleriz.
    public virtual async Task<PagedResultDto<ListOzelKodDto>> GetListAsync(OzelKodListParameterDto input)
    {
        var entities = await _ozelKodRepository.GetPagedListAsync(
            input.SkipCount,
            input.MaxResultCount,
            x => x.KodTuru == input.KodTuru && x.KartTuru == input.KartTuru && x.Durum == input.Durum,        // predicate
            x => x.Kod                        // orderby
            );

        var totalCount = await _ozelKodRepository.CountAsync(x => x.KodTuru == input.KodTuru 
            && x.KartTuru == input.KartTuru && x.Durum == input.Durum);

        return new PagedResultDto<ListOzelKodDto>(totalCount,
            ObjectMapper.Map<List<OzelKod>, List<ListOzelKodDto>>(entities));
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
    [Authorize(OnMuhasebePermissions.OzelKod.Create)]
    public virtual async Task<SelectOzelKodDto> CreateAsync(CreateOzelKodDto input)
    {
        await _ozelKodManager.CheckCreateAsync(input.Kod, input.KodTuru, input.KartTuru);

        var entity = ObjectMapper.Map<CreateOzelKodDto, OzelKod>(input);
        await _ozelKodRepository.InsertAsync(entity);
        return ObjectMapper.Map<OzelKod, SelectOzelKodDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    [Authorize(OnMuhasebePermissions.OzelKod.Update)]
    public virtual async Task<SelectOzelKodDto> UpdateAsync(Guid id, UpdateOzelKodDto input)
    {
        var entity = await _ozelKodRepository.GetAsync(id, x => x.Id == id);

        await _ozelKodManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _ozelKodRepository.UpdateAsync(mappedEntity);

        return ObjectMapper.Map<OzelKod, SelectOzelKodDto>(mappedEntity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    [Authorize(OnMuhasebePermissions.OzelKod.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _ozelKodManager.CheckDeleteAsync(id);
        await _ozelKodRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(OzelKodCodeParameterDto input)
    {
        return await _ozelKodRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}
