namespace Glipotions.OnMuhasebe.Hizmetler;

public class HizmetHareketAppService : OnMuhasebeAppService, IHizmetHareketAppService
{
    private readonly IFaturaHareketRepository _faturaHareketRepository;

    public HizmetHareketAppService(IFaturaHareketRepository faturaHareketRepository)
    {
        _faturaHareketRepository = faturaHareketRepository;
    }

    public virtual async Task<PagedResultDto<ListHizmetHareketDto>> GetListAsync(HizmetHareketListParameterDto input)
    {
        var hareketler = await _faturaHareketRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.HizmetId == input.HizmetId &&
                 x.Fatura.SubeId == input.SubeId &&
                 x.Fatura.DonemId == input.DonemId &&
                 x.Fatura.Durum,
            x => x.Fatura.Tarih,
            x => x.Fatura, x => x.Hizmet.Birim);

        var totalCount = await _faturaHareketRepository.CountAsync(x => x.HizmetId == input.HizmetId &&
                                                                        x.Fatura.SubeId == input.SubeId &&
                                                                        x.Fatura.DonemId == input.DonemId &&
                                                                        x.Fatura.Durum);

        var mappedDtos = ObjectMapper.Map<List<FaturaHareket>, List<ListHizmetHareketDto>>(hareketler);
        mappedDtos.ForEach(x =>
        {
            x.BelgeTuru = L[$"Enum:FaturaTuru:{(byte)x.FaturaTuru}"];
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });

        return new PagedResultDto<ListHizmetHareketDto>(totalCount, mappedDtos);
    }

    public Task<SelectFaturaHareketDto> GetAsync(Guid id) => throw new NotImplementedException();
    
    public Task<SelectFaturaHareketDto> CreateAsync(FaturaHareketDto input) => throw new NotImplementedException();

    public Task<SelectFaturaHareketDto> UpdateAsync(Guid id, FaturaHareketDto input) => throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<string> GetCodeAsync(FaturaNoParameterDto input) => throw new NotImplementedException();
}