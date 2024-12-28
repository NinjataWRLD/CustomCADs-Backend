using CustomCADs.Accounts.Application.Roles.DomainEventHandlers;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers;

using static Constants.Roles;

public class RoleEditedHandlerUnitTests : RolesBaseUnitTests
{
    private static ICacheService cache;

    [SetUp]
    public void SetUp()
    {
        cache = Substitute.For<ICacheService>();
    }

    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Role role = CreateRole(name, description);
        RoleEditedDomainEvent de = new(role.Id, role);
        RoleEditedEventHandler handler = new(cache);

        // Act
        await handler.Handle(de);

        // Assert
        await cache.Received(1).RemoveAsync<IEnumerable<Role>>("roles");
        await cache.Received(1).SetRangeAsync(
            ($"roles/{de.Role.Id}", de.Role),
            ($"roles/{de.Role.Name}", de.Role)
        );
    }
}
