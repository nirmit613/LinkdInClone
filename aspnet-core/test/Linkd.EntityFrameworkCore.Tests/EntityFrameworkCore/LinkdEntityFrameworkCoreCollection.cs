using Xunit;

namespace Linkd.EntityFrameworkCore;

[CollectionDefinition(LinkdTestConsts.CollectionDefinitionName)]
public class LinkdEntityFrameworkCoreCollection : ICollectionFixture<LinkdEntityFrameworkCoreFixture>
{

}
