using CustomCADs.Carts.Application.ActiveCarts.Dtos;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Events.Catalog;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Cads.Commands;
using CustomCADs.Shared.Application.UseCases.Products.Queries;
using CustomCADs.Shared.Domain.TypedIds.Accounts;
using CustomCADs.Shared.Domain.TypedIds.Carts;
using CustomCADs.Shared.Domain.TypedIds.Catalog;
using CustomCADs.Shared.Domain.TypedIds.Files;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Commands.Internal.Create;

using static PurchasedCartsData;

public class CreatePurchasedCartHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly CreatePurchasedCartHandler handler;
	private readonly Mock<IWrites<PurchasedCart>> writes = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private static readonly AccountId buyerId = AccountId.New();
	private static readonly ActiveCartItemDto[] items = [];
	private static readonly ProductId[] productIds = [.. items.Select(x => x.ProductId)];
	private static readonly Dictionary<ProductId, decimal> prices = [];
	private static readonly Dictionary<ProductId, CadId> cads = [];
	private static readonly CadId[] cadIds = [.. cads.Select(x => x.Value)];

	public CreatePurchasedCartHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, sender.Object, raiser.Object);

		writes.Setup(x => x.AddAsync(
			It.Is<PurchasedCart>(x => x.BuyerId == buyerId),
			ct
		)).ReturnsAsync(CreateCartWithId(id: ValidId));

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId),
			ct
		)).ReturnsAsync(true);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetProductCadIdsByIdsQuery>(x => x.Ids == productIds),
			ct
		)).ReturnsAsync(cads);

		sender.Setup(x => x.SendCommandAsync(
			It.Is<DuplicateCadsByIdsCommand>(x => x.Ids == cadIds),
			ct
		)).ReturnsAsync([]);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<PurchasedCart>(x => x.BuyerId == buyerId),
			ct
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
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

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountExistsByIdQuery>(x => x.Id == buyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetProductCadIdsByIdsQuery>(x => x.Ids == productIds),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.Is<DuplicateCadsByIdsCommand>(x => x.Ids == cadIds),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserPurchasedProductApplicationEvent>(x => x.Ids == productIds)
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);

		// Act
		PurchasedCartId result = await handler.Handle(command, ct);

		// Assert
		Assert.Equal(ValidId, result);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenPurchasedCartNotFound()
	{
		// Arrange
		sender.Setup(x => x.SendQueryAsync(
			It.IsAny<GetAccountExistsByIdQuery>(),
			ct
		)).ReturnsAsync(false);

		CreatePurchasedCartCommand command = new(
			BuyerId: buyerId,
			Items: items,
			Prices: prices
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
