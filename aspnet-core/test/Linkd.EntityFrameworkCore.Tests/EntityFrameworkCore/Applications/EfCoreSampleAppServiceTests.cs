using Linkd.Samples;
using Xunit;

namespace Linkd.EntityFrameworkCore.Applications;

[Collection(LinkdTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<LinkdEntityFrameworkCoreTestModule>
{

}
