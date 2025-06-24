using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetById;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetById;

using static RolesData;

public class GetRoleByIdHandlerUnitTests : RolesBaseUnitTests
{
	private readonly GetRoleByIdHandler handler;
	private readonly Mock<IRoleReads> reads = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();

	public GetRoleByIdHandlerUnitTests()
	{
		handler = new(reads.Object, cache.Object);

		cache.Setup(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Role>>>()
		)).ReturnsAsync(CreateRole());
	}

	[Fact]
	public async Task Handle_ShouldReadCache()
	{
		// Arrange
		GetRoleByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		cache.Verify(x => x.GetOrCreateAsync(
			ValidId,
			It.IsAny<Func<Task<Role>>>()
		), Times.Once);
	}
}
