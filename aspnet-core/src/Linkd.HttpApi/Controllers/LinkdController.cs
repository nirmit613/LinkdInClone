using Linkd.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Linkd.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class LinkdController : AbpControllerBase
{
    protected LinkdController()
    {
        LocalizationResource = typeof(LinkdResource);
    }
}
