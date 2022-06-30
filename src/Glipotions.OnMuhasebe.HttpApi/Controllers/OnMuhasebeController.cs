using Glipotions.OnMuhasebe.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Glipotions.OnMuhasebe.Controllers;

/* Inherit your controllers from this class.
 */
/// <ÖZET>
/// Kendi oluşturduğumuz controllerlar buraya yazılır. 
/// ancak bu projede abp nin controllerları kullanıldı
public abstract class OnMuhasebeController : AbpControllerBase
{
    protected OnMuhasebeController()
    {
        LocalizationResource = typeof(OnMuhasebeResource);
    }
}
