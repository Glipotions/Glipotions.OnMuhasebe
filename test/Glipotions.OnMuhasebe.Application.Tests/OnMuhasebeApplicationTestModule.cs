using Volo.Abp.Modularity;

namespace Glipotions.OnMuhasebe;

[DependsOn(
    typeof(OnMuhasebeApplicationModule),
    typeof(OnMuhasebeDomainTestModule)
    )]
public class OnMuhasebeApplicationTestModule : AbpModule
{

}
