using Volo.Abp.Application.Dtos;

namespace Glipotions.Blazor.Core.Services;

public interface ICoreEditPageService<TDataSource>
{
    public TDataSource DataSource { get; set; }
    /// <ÖZET>
    /// (3/5) 37. Video dk 59
    void ButtonEditDeleteKeyDown(IEntityDto entity, string fieldName);
}