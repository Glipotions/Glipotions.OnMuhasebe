using Glipotions.OnMuhasebe.Blazor.Services.Base;
using Glipotions.OnMuhasebe.Cariler;
using Glipotions.OnMuhasebe.Faturalar;
using Glipotions.OnMuhasebe.Makbuzlar;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;

namespace Glipotions.OnMuhasebe.Blazor.Services;

public class CariService : BaseService<ListCariDto, SelectCariDto>, IScopedDependency
{
    public override void SelectEntity(IEntityDto targetEntity)
    {
        switch (targetEntity)
        {
            case SelectFaturaDto fatura:
                fatura.CariId = SelectedItem.Id;
                fatura.CariAdi = SelectedItem.Ad;
                break;

            case SelectMakbuzDto makbuz:
                makbuz.CariId = SelectedItem.Id;
                makbuz.CariAdi = SelectedItem.Ad;
                break;
        }
    }
}