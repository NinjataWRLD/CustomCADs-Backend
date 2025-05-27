using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Events.Domain;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted;

using static CachingKeys;

public class RoleDeletedHandlerUnitTests : RolesBaseUnitTests
{
	private readonly Mock<ICacheService> cache = new();

	[Theory]
	[ClassData(typeof(RoleDeletedValidData))]
	public async Task Handle_ShouldUpdateCache(string name, string description)
	{
		// Arrange
		Role role = CreateRole(name, description);
		RoleDeletedDomainEvent de = new(role.Id, role.Name);
		RoleDeletedEventHandler handler = new(cache.Object);

		// Act
		await handler.Handle(de);

		// Assert
		cache.Verify(x => x.RemoveAsync<IEnumerable<Role>>(RoleKey), Times.Once);
		cache.Verify(x => x.RemoveAsync<Role>($"{RoleKey}/{de.Id}"), Times.Once);
		cache.Verify(x => x.RemoveAsync<Role>($"{RoleKey}/{de.Name}"), Times.Once);
	}
}
