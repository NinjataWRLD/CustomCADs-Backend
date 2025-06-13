using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetByName;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetByName;

using Data;
using static CachingKeys;
using static RolesData;

public class GetRoleByNameHandlerUnitTests : RolesBaseUnitTests
{
	private readonly GetRoleByNameHandler handler;
	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<ICacheService> cache = new();

	public GetRoleByNameHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		reads.Setup(x => x.SingleByNameAsync(ValidName, false, ct)).ReturnsAsync(CreateRole(ValidName, ValidDescription));
		reads.Setup(x => x.SingleByNameAsync(MinValidName, false, ct)).ReturnsAsync(CreateRole(MinValidName, MinValidDescription));
		reads.Setup(x => x.SingleByNameAsync(MaxValidName, false, ct)).ReturnsAsync(CreateRole(MaxValidName, MaxValidDescription));

		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidName}")).ReturnsAsync(CreateRole(ValidName, ValidDescription));
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{MinValidName}")).ReturnsAsync(CreateRole(MinValidName, MinValidDescription));
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{MaxValidName}")).ReturnsAsync(CreateRole(MaxValidName, MaxValidDescription));
	}

	[Theory]
	[ClassData(typeof(GetRoleByNameValidData))]
	public async Task Handle_ShouldCallCache_WhenCacheHit(string name)
	{
		// Arrange
		GetRoleByNameQuery query = new(name);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetAsync<Role>($"{RoleKey}/{name}"), Times.Once);
	}

	[Theory]
	[ClassData(typeof(GetRoleByNameValidData))]
	public async Task Handle_ShouldQueryDatabase_WhenCacheMiss(string name)
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);
		GetRoleByNameQuery query = new(name);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByNameAsync(name, false, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(GetRoleByNameValidData))]
	public async Task Handle_ShouldUpdateCache_WhenDatabaseHit(string name)
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);
		GetRoleByNameQuery query = new(name);

		// Act
		await handler.Handle(query, ct);

		// Assert
		Role role = CreateRole(name);
		cache.Verify(x => x.SetAsync(
			It.Is<string>(key => key == $"{RoleKey}/{role.Id}"),
			It.Is<Role>(item => item.Name == role.Name)
		), Times.Once);
		cache.Verify(x => x.SetAsync(
			It.Is<string>(key => key == $"{RoleKey}/{role.Name}"),
			It.Is<Role>(item => item.Name == role.Name)
		), Times.Once);
	}

	[Theory]
	[ClassData(typeof(GetRoleByNameValidData))]
	public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string name)
	{
		// Arrange
		cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);
		reads.Setup(x => x.SingleByNameAsync(name, false, ct)).ReturnsAsync(null as Role);
		GetRoleByNameQuery query = new(name);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Role>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
