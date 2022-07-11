namespace Glipotions.OnMuhasebe.Masraflar;

public class MasrafHareketAppService : OnMuhasebeAppService, IMasrafHareketAppService
{
    private readonly IFaturaHareketRepository _faturaHareketRepository;

    public MasrafHareketAppService(IFaturaHareketRepository faturaHareketRepository)
    {
        _faturaHareketRepository = faturaHareketRepository;
    }

    public virtual async Task<PagedResultDto<ListMasrafHareketDto>> GetListAsync(MasrafHareketListParameterDto input)
    {
        var hareketler = await _faturaHareketRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount,
            x => x.MasrafId == input.MasrafId &&
                 x.Fatura.SubeId == input.SubeId &&
                 x.Fatura.DonemId == input.DonemId &&
                 x.Fatura.Durum,
            x => x.Fatura.Tarih,
            x => x.Fatura, x => x.Masraf.Birim);

        var totalCount = await _faturaHareketRepository.CountAsync(x => x.MasrafId == input.MasrafId &&
                                                                        x.Fatura.SubeId == input.SubeId &&
                                                                        x.Fatura.DonemId == input.DonemId &&
                                                                        x.Fatura.Durum);

        var mappedDtos = ObjectMapper.Map<List<FaturaHareket>, List<ListMasrafHareketDto>>(hareketler);
        mappedDtos.ForEach(x =>
        {
            x.BelgeTuru = L[$"Enum:FaturaTuru:{(byte)x.FaturaTuru}"];
            x.HareketTuruAdi = L[$"Enum:FaturaHareketTuru:{(byte)x.HareketTuru}"];
        });

        return new PagedResultDto<ListMasrafHareketDto>(totalCount, mappedDtos);
    }
    
    public Task<SelectFaturaHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public Task<SelectFaturaHareketDto> CreateAsync(FaturaHareketDto input) => throw new NotImplementedException();

    public Task<SelectFaturaHareketDto> UpdateAsync(Guid id, FaturaHareketDto input) => throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<string> GetCodeAsync(FaturaNoParameterDto input) => throw new NotImplementedException();
}