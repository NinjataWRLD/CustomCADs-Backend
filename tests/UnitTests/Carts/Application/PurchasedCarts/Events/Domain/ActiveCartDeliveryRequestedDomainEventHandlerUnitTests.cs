using CustomCADs.Carts.Application.PurchasedCarts.Events.Domain;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Events.Domain;

using static PurchasedCartsData;

public class ActiveCartDeliveryRequestedDomainEventHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly PurchasedCart cart = CreateCartWithItems(
		items: [CreateItem(forDelivery: true)]
	);
	private readonly PurchasedCartId id = ValidId1;

	public ActiveCartDeliveryRequestedDomainEventHandlerUnitTests()
	{
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
			.ReturnsAsync("NinjataBG");

		sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateShipmentCommand>(), ct))
			.ReturnsAsync(ValidShipmentId1);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ActiveCartDeliveryRequestedDomainEvent de = new(
			Id: id,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty),
			Contact: new(default, default)
		);
		ActiveCartDeliveryRequestedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(de);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ActiveCartDeliveryRequestedDomainEvent de = new(
			Id: id,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty),
			Contact: new(default, default)
		);
		ActiveCartDeliveryRequestedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Act
		await handler.Handle(de);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetUsernameByIdQuery>(),
		ct), Times.Once);
		sender.Verify(x => x.SendCommandAsync(
			It.IsAny<CreateShipmentCommand>(),
		ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenCartNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(id, false, ct))
			.ReturnsAsync(null as PurchasedCart);

		ActiveCartDeliveryRequestedDomainEvent de = new(
			Id: id,
			ShipmentService: string.Empty,
			Weight: default,
			Count: default,
			Address: new(string.Empty, string.Empty),
			Contact: new(default, default)
		);
		ActiveCartDeliveryRequestedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(async () =>
		{
			// Act
			await handler.Handle(de);
		});
	}
}
