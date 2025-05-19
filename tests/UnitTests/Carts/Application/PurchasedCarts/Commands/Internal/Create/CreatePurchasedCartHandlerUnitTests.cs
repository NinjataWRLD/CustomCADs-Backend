using CustomCADs.Carts.Application.ActiveCarts.Dtos;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Commands.Internal.Create;

public class CreatePurchasedCartHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly Mock<IWrites<PurchasedCart>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private static readonly AccountId buyerId = AccountId.New();
	private static readonly ActiveCartItemDto[] items = [];
	private static readonly Dictionary<ProductId, decimal> prices = [];

	public CreatePurchasedCartHandlerUnitTests()
	{
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
			.ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductCadIdsByIdsQuery>(), ct))
			.ReturnsAsync([]);

		sender.Setup(x => x.SendCommandAsync(It.IsAny<DuplicateCadsByIdsCommand>(), ct))
			.ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatbase()
	{
		// Arrange
		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);
		CreatePurchasedCartHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(It.IsAny<PurchasedCart>(), ct));
		uow.Verify(x => x.SaveChangesAsync(ct));
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);
		CreatePurchasedCartHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetAccountExistsByIdQuery>()
		, ct), Times.Once);
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetProductCadIdsByIdsQuery>()
		, ct), Times.Once);
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<DuplicateCadsByIdsCommand>()
		, ct), Times.Once);
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<AddProductPurchaseCommand>()
		, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetAccountExistsByIdQuery>(), ct))
			.ReturnsAsync(false);

		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);
		CreatePurchasedCartHandler handler = new(writes.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(async () =>
		{
			// Act
			await handler.Handle(command, ct);
		});
	}
}
