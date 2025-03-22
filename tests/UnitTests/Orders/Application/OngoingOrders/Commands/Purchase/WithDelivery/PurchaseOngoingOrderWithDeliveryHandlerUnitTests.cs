using CustomCADs.Orders.Application.Common.Exceptions.Ongoing;
using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Orders.Application.CompletedOrders.Queries.ClientGetById;
using CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;
using CustomCADs.Orders.Domain.OngoingOrders.Events;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

using static OngoingOrdersData;

public class PurchaseOngoingOrderWithDeliveryHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Mock<IPaymentService> payment = new();
    private readonly Mock<IEventRaiser> raiser = new();

    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private static readonly ContactDto contact = new(null, null);
    private static readonly OngoingOrder order = CreateOrder(
        buyerId: buyerId,
        delivery: true
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus().SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderNotFinished = CreateOrder(
        buyerId: buyerId
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderWithoutDelivery = CreateOrder(
        buyerId: buyerId,
        delivery: false
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus().SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderWithoutPrice = CreateOrder(
        buyerId: buyerId,
        delivery: true
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus();

    public PurchaseOngoingOrderWithDeliveryHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationCostByIdQuery>(), ct))
            .ReturnsAsync(0m);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<ClientGetCompletedOrderByIdQuery>(), ct))
            .ReturnsAsync(new ClientGetCompletedOrderByIdDto(
                Id: default,
                Name: string.Empty,
                Description: string.Empty,
                Delivery: default,
                OrderDate: default,
                PurchaseDate: default,
                DesignerName: string.Empty,
                ShipmentId: default
            ));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreateCompletedOrderCommand>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Exactly(2));
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationCostByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationWeightByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        payment.Verify(x => x.InitializePayment(
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<string>(),
            ct
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldRaiseEvents()
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.IsAny<OngoingOrderDeliveryRequestedDomainEvent>()
        ), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        const string expected = "Payment Status Message";
        payment.Setup(x => x.InitializePayment(
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<string>(),
            ct
        )).ReturnsAsync(expected);

        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        string actual = await handler.Handle(command, ct);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: wrongBuyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderAuthorizationException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNotFinished()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(orderNotFinished);

        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderStatusException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNoForDelivery()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(orderWithoutDelivery);

        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderDeliveryException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenNoPrice()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(orderWithoutPrice);

        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderPriceException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenOrderNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(null as OngoingOrder);

        PurchaseOngoingOrderWithDeliveryCommand command = new(
            OrderId: id,
            Count: default,
            CustomizationId: default,
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseOngoingOrderWithDeliveryHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<OngoingOrderNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
