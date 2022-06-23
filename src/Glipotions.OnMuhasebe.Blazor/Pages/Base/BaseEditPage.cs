using Glipotions.OnMuhasebe.Localization;
using Microsoft.AspNetCore.Components;
using Volo.Abp.AspNetCore.Components;

namespace Glipotions.OnMuhasebe.Blazor.Pages.Base;

public abstract class BaseEditPage : AbpComponentBase
{
    public BaseEditPage()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }

    [Parameter] public EventCallback OnSubmit { get; set; }
}