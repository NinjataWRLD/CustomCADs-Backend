using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetById;

using static CachingKeys;
using static RolesData;

public class GetRoleByIdHandlerUnitTests : RolesBaseUnitTests
{
	private readonly GetRoleByIdHandler handler;
	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<ICacheService> cache = new();

	public GetRoleByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateRole(ValidName, ValidDescription));
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidId}")).ReturnsAsync(CreateRole(ValidName, ValidDescription));
	}

	[Fact]
	public async Task Handle_ShouldCallCache_WhenCacheHit()
	{
		// Arrange
		GetRoleByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetAsync<Role>($"{RoleKey}/{ValidId}"), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase_WhenCacheMiss()
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidId}")).ReturnsAsync(null as Role);
		GetRoleByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache_WhenDatabaseHit()
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidId}")).ReturnsAsync(null as Role);
		GetRoleByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		Role role = CreateRole(ValidName);
		cache.Verify(x => x.SetAsync(
			It.Is<string>(key => key == $"{RoleKey}/{role.Id}"),
			It.Is<Role>(item => item.Name == role.Name)
		), Times.Once);
		cache.Verify(x => x.SetAsync(
			It.Is<string>(key => key == $"{RoleKey}/{role.Name}"),
			It.Is<Role>(item => item.Name == role.Name)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenDatabaseMiss()
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidId}")).ReturnsAsync(null as Role);
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Role);
		GetRoleByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Role>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
