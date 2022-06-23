using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Parametreler;
using Glipotions.OnMuhasebe.Subeler;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class SubeService : BaseService<ListSubeDto, SelectSubeDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectFirmaParametreDto firmaParametre:
                firmaParametre.SubeId = SelectedItem.Id;
                firmaParametre.SubeAdi = SelectedItem.Ad;
                break;
        }
    }
}