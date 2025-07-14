using CustomCADs.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.ApplicationEvents.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly UserViewedProductHandler handler;
	private readonly Mock<IAccountWrites> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly AccountId id = AccountId.New();
	private static readonly ProductId productId = ProductId.New();

	public UserViewedProductHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		UserViewedProductApplicationEvent ie = new(id, productId);

		// Act
		await handler.Handle(ie);

		// Assert
		writes.Verify(x => x.ViewProductAsync(id, productId, ct), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}
}
