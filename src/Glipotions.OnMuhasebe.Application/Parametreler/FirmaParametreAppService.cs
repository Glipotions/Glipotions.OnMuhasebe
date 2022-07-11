using Microsoft.AspNetCore.Mvc;

namespace Glipotions.OnMuhasebe.Parametreler;
[Authorize]
public class FirmaParametreAppService : OnMuhasebeAppService, IFirmaParametreAppService
{
    private readonly IFirmaParametreRepository _firmaParametreRepository;
    private readonly FirmaParametreManager _firmaParametreManager;

    public FirmaParametreAppService(IFirmaParametreRepository firmaParametreRepository, FirmaParametreManager firmaParametreManager)
    {
        _firmaParametreRepository = firmaParametreRepository;
        _firmaParametreManager = firmaParametreManager;
    }

    /// <Özet>
    /// Parametre olarak gönderilen id'nin entity.id ye eşit olduğu entity'i geriye döndürür.
    /// ObjectMapper Entity'i Select(Entity)Dto olarak mapler.
    public virtual async Task<SelectFirmaParametreDto> GetAsync(Guid userId)
    {
        var entity = await _firmaParametreRepository.GetAsync(x => x.UserId == userId, x => x.Sube, x => x.Donem, x => x.Depo);

        if (entity == null) return null;

        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(entity);
    }
    /// <Özet>
    /// NonAction bunun kullanılmasını engeller. Liste olarak çağırmayacağımız için NonAction imzası atıldı.
    [NonAction]
    public virtual Task<PagedResultDto<SelectFirmaParametreDto>> GetListAsync(
        FirmaParametreListParameterDto input)
    {
        throw new NotImplementedException();
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
    public virtual async Task<SelectFirmaParametreDto> CreateAsync(CreateFirmaParametreDto input)
    {
        await _firmaParametreManager.CheckCreateAsync(input.SubeId, input.DonemId,
            input.DepoId);

        var entity = ObjectMapper.Map<CreateFirmaParametreDto, FirmaParametre>(input);
        await _firmaParametreRepository.InsertAsync(entity);
        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(entity);
    }
    /// <Özet>
    /// CheckUpdateAsync ile Manager sınıfından database kontrolü yapılır.
    /// Maplerken Elimizde 2 entity var o yüzden generic yapı kullanılmaz. 
    /// UI dan gelen input ile entity maplenir. Arada oluşan farklar update edilir.
    /// <param name="id"></param>
    /// <param name="input"></param> UI dan gelir
    /// <returns> Maplenmiş entity return edilir. </returns>
    public virtual async Task<SelectFirmaParametreDto> UpdateAsync(Guid userId, UpdateFirmaParametreDto input)
    {
        await _firmaParametreManager.CheckUpdateAsync(input.SubeId, input.DonemId,
            input.DepoId);

        var entity = await _firmaParametreRepository.GetAsync(userId, x => x.UserId == userId);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _firmaParametreRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<FirmaParametre, SelectFirmaParametreDto>(mappedEntity);
    }
    [NonAction]
    public virtual async Task<bool> UserAnyAsync(Guid userId)
    {
        return await _firmaParametreRepository.AnyAsync(x => x.UserId == userId);
    }
}
