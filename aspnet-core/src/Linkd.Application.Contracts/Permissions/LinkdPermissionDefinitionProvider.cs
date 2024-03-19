using Linkd.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Linkd.Permissions;

public class LinkdPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(LinkdPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(LinkdPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LinkdResource>(name);
    }
}
