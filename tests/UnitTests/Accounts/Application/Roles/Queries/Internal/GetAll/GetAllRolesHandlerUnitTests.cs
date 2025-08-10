using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetAll;

using static Constants.Roles;

public class GetAllRolesHandlerUnitTests : RolesBaseUnitTests
{
	private readonly GetAllRolesHandler handler;
	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();

	private readonly Role[] roles = [
		Role.CreateWithId(RoleId.New(), Customer, CustomerDescription),
		Role.CreateWithId(RoleId.New(), Contributor, ContributorDescription),
		Role.CreateWithId(RoleId.New(), Designer, DesignerDescription),
		Role.CreateWithId(RoleId.New(), Admin, AdminDescription),
	];

	public GetAllRolesHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			It.IsAny<Func<Task<ICollection<Role>>>>())
		).ReturnsAsync(roles);
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetAllRolesQuery query = new();

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(
			It.IsAny<Func<Task<ICollection<Role>>>>()),
			Times.Once()
		);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAllRolesQuery query = new();

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(roles.Select(x => x.Id), result.Select(x => x.Id));
	}
}
