namespace Glipotions.OnMuhasebe.Kasalar;

public class KasaHareketAppService : OnMuhasebeAppService, IKasaHareketAppService
{
    private readonly IMakbuzHareketRepository _makbuzHareketRepository;

    public KasaHareketAppService(IMakbuzHareketRepository makbuzHareketRepository)
    {
        _makbuzHareketRepository = makbuzHareketRepository;
    }
    /// <ÖZET>
    /// (5/5) 9. video
    public virtual async Task<PagedResultDto<ListOdemeBelgesiHareketDto>> GetListAsync(
        MakbuzHareketListParameterDto input)
    {
        var hareketler = await _makbuzHareketRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.KasaId == input.EntityId &&
                 x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId && x.Makbuz.Durum,
            x => x.Makbuz.Tarih,
            x => x.Makbuz);

        var totalCount = await _makbuzHareketRepository.CountAsync(x => x.KasaId == input.EntityId &&
                                                                        x.Makbuz.SubeId == input.SubeId &&
                                                                        x.Makbuz.DonemId == input.DonemId &&
                                                                        x.Makbuz.Durum);

        var mappedDtos = ObjectMapper.Map<List<MakbuzHareket>, List<ListOdemeBelgesiHareketDto>>(hareketler);
        mappedDtos.ForEach(x =>
        {
            x.OdemeTuruAdi = L[$"Enum:OdemeTuru:{(byte)x.OdemeTuru}"];
            x.MakbuzTuruAdi = L[$"Enum:MakbuzTuru:{(byte)x.MakbuzTuru}"];
            x.BelgeDurumuAdi = L[$"Enum:BelgeDurumu:{(byte)x.BelgeDurumu}"];
        });

        return new PagedResultDto<ListOdemeBelgesiHareketDto>(totalCount, mappedDtos);
    }

    public Task<SelectMakbuzHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> CreateAsync(MakbuzHareketDto input) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> UpdateAsync(Guid id, MakbuzHareketDto input) =>
        throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<string> GetCodeAsync(MakbuzNoParameterDto input) => throw new NotImplementedException();
}