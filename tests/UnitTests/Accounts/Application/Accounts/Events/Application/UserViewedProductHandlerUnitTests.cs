using CustomCADs.Accounts.Application.Accounts.Events.Application;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.ApplicationEvents.Catalog;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Events.Application;

public class UserViewedProductHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly UserViewedProductHandler handler;
	private readonly Mock<IAccountReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();

	private static readonly AccountId id = AccountId.New();
	private static readonly ProductId productId = ProductId.New();
	private readonly Account account = CreateAccount();

	public UserViewedProductHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object);
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		UserViewedProductApplicationEvent ie = new(id, productId);

		// Act
		await handler.Handle(ie);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		UserViewedProductApplicationEvent ie = new(id, productId);

		// Act
		await handler.Handle(ie);

		// Assert
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldPopulateProperly()
	{
		// Arrange
		UserViewedProductApplicationEvent ie = new(id, productId);

		// Act
		await handler.Handle(ie);

		// Assert
		Assert.Contains(account.ViewedProductIds, x => x == productId);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, true, ct))
			.ReturnsAsync(null as Account);
		UserViewedProductApplicationEvent ie = new(id, productId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(ie)
		);
	}
}
