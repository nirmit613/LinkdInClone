using System;
using System.Collections.Generic;
using System.Text;
using Linkd.Localization;
using Volo.Abp.Application.Services;

namespace Linkd;

/* Inherit your application services from this class.
 */
public abstract class LinkdAppService : ApplicationService
{
    protected LinkdAppService()
    {
        LocalizationResource = typeof(LinkdResource);
    }
}
