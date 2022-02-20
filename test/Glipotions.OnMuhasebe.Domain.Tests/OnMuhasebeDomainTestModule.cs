using Glipotions.OnMuhasebe.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Glipotions.OnMuhasebe;

[DependsOn(
    typeof(OnMuhasebeEntityFrameworkCoreTestModule)
    )]
public class OnMuhasebeDomainTestModule : AbpModule
{

}
