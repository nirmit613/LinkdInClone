using Linkd.Samples;
using Xunit;

namespace Linkd.EntityFrameworkCore.Domains;

[Collection(LinkdTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<LinkdEntityFrameworkCoreTestModule>
{

}
