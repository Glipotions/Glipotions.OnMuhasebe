using Glipotions.OnMuhasebe.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Glipotions.OnMuhasebe.Permissions;

public class OnMuhasebePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(OnMuhasebePermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(OnMuhasebePermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<OnMuhasebeResource>(name);
    }
}
