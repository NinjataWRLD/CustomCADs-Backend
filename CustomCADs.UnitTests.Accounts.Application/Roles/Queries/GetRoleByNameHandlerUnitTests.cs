using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries;

using static Constants.Roles;

public class GetRoleByNameHandlerUnitTests : RolesBaseUnitTests
{
    private static IRoleReads reads;
    private static ICacheService cache;

    [SetUp]
    public void Setup()
    {
        reads = Substitute.For<IRoleReads>();
        reads.SingleByNameAsync(Client, track: false, ct).Returns(CreateRole(Client, ClientDescription));
        reads.SingleByNameAsync(Contributor, track: false, ct).Returns(CreateRole(Contributor, ContributorDescription));
        reads.SingleByNameAsync(Designer, track: false, ct).Returns(CreateRole(Designer, DesignerDescription));
        reads.SingleByNameAsync(Admin, track: false, ct).Returns(CreateRole(Admin, AdminDescription));

        cache = Substitute.For<ICacheService>();
        cache.GetAsync<Role>($"role/{Client}").Returns(CreateRole(Client, ClientDescription));
        cache.GetAsync<Role>($"role/{Contributor}").Returns(CreateRole(Contributor, ContributorDescription));
        cache.GetAsync<Role>($"role/{Designer}").Returns(CreateRole(Designer, DesignerDescription));
        cache.GetAsync<Role>($"role/{Admin}").Returns(CreateRole(Admin, AdminDescription));
    }

    [Test]
    [TestCase(Client)]
    [TestCase(Contributor)]
    [TestCase(Designer)]
    [TestCase(Admin)]
    public async Task Handle_ShouldPullFromCache_WhenCacheHit(string name)
    {
        // Assert
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await cache.Received(1).GetAsync<Role>($"roles/{name}");
    }

    [Test]
    [TestCase(Client)]
    [TestCase(Contributor)]
    [TestCase(Designer)]
    [TestCase(Admin)]
    public async Task Handle_ShouldCallDatabase_WhenCacheMiss(string name)
    {
        // Assert
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(name, track: false, ct);
    }

    [Test]
    [TestCase(Client)]
    [TestCase(Contributor)]
    [TestCase(Designer)]
    [TestCase(Admin)]
    public void Handle_ShouldThrowException_WhenDatabaseMiss(string name)
    {
        // Assert
        reads.SingleByNameAsync(name, false, ct).Returns(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Assert
        Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
