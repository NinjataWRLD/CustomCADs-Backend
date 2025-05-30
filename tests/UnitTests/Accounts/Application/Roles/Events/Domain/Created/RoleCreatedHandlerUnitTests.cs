using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Events.Domain;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Events.Domain.Created;

using Data;
using static CachingKeys;

public class RoleCreatedHandlerUnitTests : RolesBaseUnitTests
{
	private readonly RoleCreatedEventHandler handler;
	private readonly Mock<ICacheService> cache = new();

	public RoleCreatedHandlerUnitTests()
	{
		handler = new(cache.Object);
	}

	[Theory]
	[ClassData(typeof(RoleCreatedValidData))]
	public async Task Handle_ShouldUpdateCache(Role role)
	{
		// Arrange
		RoleCreatedDomainEvent de = new(role);

		// Act
		await handler.Handle(de);

		// Assert
		cache.Verify(x => x.RemoveAsync<IEnumerable<Role>>(RoleKey), Times.Once);
		cache.Verify(x => x.SetAsync($"{RoleKey}/{de.Role.Id}", de.Role), Times.Once);
		cache.Verify(x => x.SetAsync($"{RoleKey}/{de.Role.Name}", de.Role), Times.Once);
	}
}
