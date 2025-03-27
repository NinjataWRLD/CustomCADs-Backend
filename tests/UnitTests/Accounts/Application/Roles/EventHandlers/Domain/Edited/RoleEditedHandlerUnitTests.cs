using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.EventHandlers.Domain;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.EventHandlers.Domain.Edited.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.EventHandlers.Domain.Edited;

using static CachingKeys;

public class RoleEditedHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<ICacheService> cache = new();

    [Theory]
    [ClassData(typeof(RoleEditedValidData))]
    public async Task Handle_ShouldUpdateCache(string name, string description)
    {
        // Arrange
        Role role = CreateRole(name, description);
        RoleEditedDomainEvent de = new(role.Id, role);
        RoleEditedEventHandler handler = new(cache.Object);

        // Act
        await handler.Handle(de);

        // Assert
        cache.Verify(x => x.RemoveAsync<IEnumerable<Role>>(RoleKey), Times.Once);
        cache.Verify(x => x.SetAsync($"{RoleKey}/{de.Role.Id}", de.Role), Times.Once);
        cache.Verify(x => x.SetAsync($"{RoleKey}/{de.Role.Name}", de.Role), Times.Once);
    }
}
