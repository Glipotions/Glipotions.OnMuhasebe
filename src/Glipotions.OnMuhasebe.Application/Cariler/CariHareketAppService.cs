using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.MakbuzHareketler;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace Glipotions.OnMuhasebe.Cariler;

public class CariHareketAppService : OnMuhasebeAppService, ICariHareketAppService
{
    private readonly IMakbuzHareketRepository _makbuzHareketRepository;
    private readonly IFaturaHareketRepository _faturaHareketRepository;

    public CariHareketAppService(IMakbuzHareketRepository makbuzHareketRepository,
        IFaturaHareketRepository faturaHareketRepository)
    {
        _makbuzHareketRepository = makbuzHareketRepository;
        _faturaHareketRepository = faturaHareketRepository;
    }

    public virtual async Task<PagedResultDto<ListCariHareketDto>> GetListAsync(CariHareketListParameterDto input)
    {
        var makbuzHareketler = await _makbuzHareketRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Makbuz.CariId == input.CariId &&
                 x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId && x.Makbuz.Durum,
            x => x.Makbuz.Tarih,
            x => x.Makbuz);

        var totalMakbuzHareketCount = await _makbuzHareketRepository.CountAsync(x =>
            x.Makbuz.CariId == input.CariId &&
            x.Makbuz.SubeId == input.SubeId && x.Makbuz.DonemId == input.DonemId && x.Makbuz.Durum);

        var mappedMakbuzHareketDtos = ObjectMapper.Map<List<MakbuzHareket>,
            List<ListCariHareketDto>>(makbuzHareketler);

        mappedMakbuzHareketDtos.ForEach(x =>
        {
            x.BelgeTuru = L["Receipt"];
            x.HareketTuru = L[$"Enum:MakbuzTuru:{(byte)x.MakbuzTuru}"];
        });

        var faturaHareketler = await _faturaHareketRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Fatura.CariId == input.CariId &&
                 x.Fatura.SubeId == input.SubeId && x.Fatura.DonemId == input.DonemId && x.Fatura.Durum,
            x => x.Fatura.Tarih,
            x => x.Fatura);

        var totalFaturaHareketCount = await _faturaHareketRepository.CountAsync(x =>
            x.Fatura.CariId == input.CariId &&
            x.Fatura.SubeId == input.SubeId && x.Fatura.DonemId == input.DonemId && x.Fatura.Durum);

        var mappedFaturaHareketDtos = ObjectMapper.Map<List<FaturaHareket>,
            List<ListCariHareketDto>>(faturaHareketler);

        mappedFaturaHareketDtos.ForEach(x =>
        {
            x.BelgeTuru = L["Invoice"];
            x.HareketTuru = L[$"Enum:FaturaTuru:{(byte)x.FaturaTuru}"];
        });

        var items = mappedFaturaHareketDtos.Concat(mappedMakbuzHareketDtos).OrderBy(x => x.Tarih).ToList();
        return new PagedResultDto<ListCariHareketDto>(totalMakbuzHareketCount + totalFaturaHareketCount, items);
    }

    public Task<SelectMakbuzHareketDto> GetAsync(Guid id) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> CreateAsync(MakbuzHareketDto input) => throw new NotImplementedException();

    public Task<SelectMakbuzHareketDto> UpdateAsync(Guid id, MakbuzHareketDto input) =>
        throw new NotImplementedException();

    public Task DeleteAsync(Guid id) => throw new NotImplementedException();

    public Task<string> GetCodeAsync(MakbuzNoParameterDto input) => throw new NotImplementedException();
}