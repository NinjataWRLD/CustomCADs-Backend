using CustomCADs.Orders.Application.CompletedOrders.EventHandlers.Domain;
using CustomCADs.Orders.Domain.OngoingOrders.Events;
using CustomCADs.Orders.Domain.Repositories;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Shipments.Commands;

namespace CustomCADs.UnitTests.Orders.Application.CompletedOrders.EventHandlers.Domain.DeliveryRequested;

using static CompletedOrdersData;

public class OngoingOrderDeliveryRequestedDomainEventHandlerUnitTests : CompletedOrdersBaseUnitTests
{
    private readonly Mock<ICompletedOrderReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();

    private const string ShipmentService = "shipment-service";
    private const double Weight = 5.2;
    private const int Count = 3;
    private static readonly CompletedOrderId id = ValidId1;
    private static readonly ShipmentId shipmentId = ValidShipmentId2;
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private static readonly ContactDto contact = new("0123456789", null);
    private static readonly CompletedOrder order = CreateOrderWithId(id, delivery: true);

    public OngoingOrderDeliveryRequestedDomainEventHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(ValidId1, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
            .ReturnsAsync("NinjataBG");

        sender.Setup(x => x.SendCommandAsync(It.IsAny<CreateShipmentCommand>(), ct))
            .ReturnsAsync(shipmentId);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        OngoingOrderDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );
        OngoingOrderDeliveryRequestedDomainEventHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(ValidId1, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        OngoingOrderDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );
        OngoingOrderDeliveryRequestedDomainEventHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreateShipmentCommand>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPopulateProperly()
    {
        // Arrange
        OngoingOrderDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );
        OngoingOrderDeliveryRequestedDomainEventHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        Assert.Equal(shipmentId, order.ShipmentId);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(ValidId1, false, ct))
            .ReturnsAsync(null as CompletedOrder);

        OngoingOrderDeliveryRequestedDomainEvent de = new(
            Id: id,
            ShipmentService: ShipmentService,
            Weight: Weight,
            Count: Count,
            Address: address,
            Contact: contact
        );
        OngoingOrderDeliveryRequestedDomainEventHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<CompletedOrder>>(async () =>
        {
            // Act
            await handler.Handle(de);
        });
    }
}
