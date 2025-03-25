using CustomCADs.Orders.Application.CompletedOrders.Commands.Create;
using CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;
using CustomCADs.Orders.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

using static OngoingOrdersData;

public class PurchaseOngoingOrderHandlerUnitTests : OngoingOrdersBaseUnitTests
{
    private readonly Mock<IOngoingOrderReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Mock<IPaymentService> payment = new();

    private static readonly OngoingOrderId id = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AccountId wrongBuyerId = ValidBuyerId2;
    private static readonly OngoingOrder order = CreateOrder(
        buyerId: buyerId
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus().SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderNotFinished = CreateOrder(
        buyerId: buyerId
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderWithDelivery = CreateOrder(
        buyerId: buyerId,
        delivery: true
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus().SetPrice(ValidPrice1);
    private static readonly OngoingOrder orderWithoutPrice = CreateOrder(
        buyerId: buyerId
    ).SetAcceptedStatus().SetDesignerId(ValidDesignerId1).SetBegunStatus().SetCadId(ValidCadId1).SetFinishedStatus();

    public PurchaseOngoingOrderHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(order);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreateCompletedOrderCommand>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Exactly(2));
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

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

        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Act
        string actual = await handler.Handle(command, ct);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenUnauthorizedAccess()
    {
        // Arrange
        PurchaseOngoingOrderCommand command = new(id, string.Empty, wrongBuyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomAuthorizationException<OngoingOrder>>(async () =>
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

        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomStatusException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenForDelivery()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, false, ct))
            .ReturnsAsync(orderWithDelivery);

        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
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

        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
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

        PurchaseOngoingOrderCommand command = new(id, string.Empty, buyerId);
        PurchaseOngoingOrderHandler handler = new(reads.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<OngoingOrder>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
