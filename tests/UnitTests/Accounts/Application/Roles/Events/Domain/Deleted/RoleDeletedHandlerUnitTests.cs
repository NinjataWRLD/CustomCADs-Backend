using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Events.Domain;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Deleted;

using Data;
using static CachingKeys;

public class RoleDeletedHandlerUnitTests : RolesBaseUnitTests
{
	private readonly RoleDeletedEventHandler handler;
	private readonly Mock<ICacheService> cache = new();

	public RoleDeletedHandlerUnitTests()
	{
		handler = new(cache.Object);
	}

	[Theory]
	[ClassData(typeof(RoleDeletedValidData))]
	public async Task Handle_ShouldUpdateCache(Role role)
	{
		// Arrange
		RoleDeletedDomainEvent de = new(role.Id, role.Name);

		// Act
		await handler.Handle(de);

		// Assert
		cache.Verify(x => x.RemoveAsync<IEnumerable<Role>>(RoleKey), Times.Once);
		cache.Verify(x => x.RemoveAsync<Role>($"{RoleKey}/{de.Id}"), Times.Once);
		cache.Verify(x => x.RemoveAsync<Role>($"{RoleKey}/{de.Name}"), Times.Once);
	}
}
