using CustomCADs.Accounts.Application.Roles.DomainEventHandlers;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers.Created.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.DomainEventHandlers.Created;

public class RoleCreatedHandlerData : TheoryData<string, string>;

public class RoleCreatedHandlerUnitTests : RolesBaseUnitTests
{
    private readonly ICacheService cache = Substitute.For<ICacheService>();

    [Theory]
    [ClassData(typeof(RoleCreatedHandlerValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Role role = CreateRole(name, description);
        RoleCreatedDomainEvent de = new(role);
        RoleCreatedEventHandler handler = new(cache);

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
