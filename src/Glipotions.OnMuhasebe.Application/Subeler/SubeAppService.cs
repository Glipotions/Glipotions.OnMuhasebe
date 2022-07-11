namespace Glipotions.OnMuhasebe.Subeler;

[Authorize(OnMuhasebePermissions.Sube.Default)]
public class SubeAppService : OnMuhasebeAppService, ISubeAppService
{
    private readonly ISubeRepository _subeRepository;
    private readonly SubeManager _subeManager;

    public SubeAppService(ISubeRepository subeRepository, SubeManager subeManager)
    {
        _subeRepository = subeRepository;
        _subeManager = subeManager;
    }

    public virtual async Task<SelectSubeDto> GetAsync(Guid id)
    {
        var entity = await _subeRepository.GetAsync(id, x => x.Id == id);
        return ObjectMapper.Map<Sube, SelectSubeDto>(entity);
    }

    public virtual async Task<PagedResultDto<ListSubeDto>> GetListAsync(
        SubeListParameterDto input)
    {
        var entities = await _subeRepository.GetPagedListAsync(input.SkipCount,
            input.MaxResultCount,
            x => x.Durum == input.Durum,
            x => x.Kod);

        var totalCount = await _subeRepository.CountAsync(x => x.Durum == input.Durum);

        return new PagedResultDto<ListSubeDto>(
            totalCount,
            ObjectMapper.Map<List<Sube>, List<ListSubeDto>>(entities)
            );
    }

    [Authorize(OnMuhasebePermissions.Sube.Create)]
    public virtual async Task<SelectSubeDto> CreateAsync(CreateSubeDto input)
    {
        await _subeManager.CheckCreateAsync(input.Kod);

        var entity = ObjectMapper.Map<CreateSubeDto, Sube>(input);
        await _subeRepository.InsertAsync(entity);
        return ObjectMapper.Map<Sube, SelectSubeDto>(entity);
    }

    [Authorize(OnMuhasebePermissions.Sube.Update)]
    public virtual async Task<SelectSubeDto> UpdateAsync(Guid id, UpdateSubeDto input)
    {
        var entity = await _subeRepository.GetAsync(id, x => x.Id == id);

        await _subeManager.CheckUpdateAsync(id, input.Kod, entity);

        var mappedEntity = ObjectMapper.Map(input, entity);
        await _subeRepository.UpdateAsync(mappedEntity);
        return ObjectMapper.Map<Sube, SelectSubeDto>(mappedEntity);
    }

    [Authorize(OnMuhasebePermissions.Sube.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _subeManager.CheckDeleteAsync(id);
        await _subeRepository.DeleteAsync(id);
    }

    public virtual async Task<string> GetCodeAsync(CodeParameterDto input)
    {
        return await _subeRepository.GetCodeAsync(x => x.Kod, x => x.Durum == input.Durum);
    }
}