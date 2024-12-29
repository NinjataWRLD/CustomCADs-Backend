using CustomCADs.Accounts.Application.Roles.DomainEventHandlers;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers;

using static Constants.Roles;

public class RoleDeletedHandlerUnitTests : RolesBaseUnitTests
{
    private readonly ICacheService cache = Substitute.For<ICacheService>();

    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Role role = CreateRole(name, description);
        RoleDeletedDomainEvent de = new(role.Id, role.Name);
        RoleDeletedEventHandler handler = new(cache);

        // Act
        await handler.Handle(de);

        // Assert
        await cache.Received(1).RemoveAsync<IEnumerable<Role>>("roles");
        await cache.Received(1).RemoveRangeAsync<Role>(
            $"roles/{de.Id}",
            $"roles/{de.Name}"
        );
    }
}
