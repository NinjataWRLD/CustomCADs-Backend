using CustomCADs.Carts.Application.ActiveCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Abstractions.Requests.Sender;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;
using CustomCADs.Shared.Application.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Events.Application.DeliveryRequested;

using static PurchasedCartsData;

public class ActiveCartDeliveryRequestedApplicationEventHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly ActiveCartDeliveryRequestedApplicationEventHandler handler;
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();

	private readonly PurchasedCart cart = CreateCartWithItems(
		items: [CreateItem(forDelivery: true)]
	);

	public ActiveCartDeliveryRequestedApplicationEventHandlerUnitTests()
	{
		handler = new(reads.Object, uow.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == cart.BuyerId),
			ct
		)).ReturnsAsync("NinjataBG");

		sender.Setup(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == cart.BuyerId),
			ct
		)).ReturnsAsync(ValidShipmentId);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ActiveCartDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty, string.Empty),
			Contact: new(default, default)
		);

		// Act
		await handler.Handle(de);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ActiveCartDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty, string.Empty),
			Contact: new(default, default)
		);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == cart.BuyerId),
			ct
		), Times.Once());
		sender.Verify(x => x.SendCommandAsync(
			It.Is<CreateShipmentCommand>(x => x.BuyerId == cart.BuyerId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as PurchasedCart);

		ActiveCartDeliveryRequestedApplicationEvent de = new(
			Id: ValidId,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty, string.Empty),
			Contact: new(default, default)
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(
			// Act
			async () => await handler.Handle(de)
		);
	}
}
