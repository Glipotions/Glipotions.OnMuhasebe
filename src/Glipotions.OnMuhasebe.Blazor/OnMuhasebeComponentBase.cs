using Glipotions.OnMuhasebe.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Glipotions.OnMuhasebe.Blazor;

public abstract class OnMuhasebeComponentBase : AbpComponentBase
{
    protected OnMuhasebeComponentBase()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }
}
