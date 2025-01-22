using CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.Normal;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Create;
using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetById;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.ActiveCarts.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Purchase.Normal;

using static ActiveCartsData;

public class PurchaseActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private const decimal TotalCost = 10m;
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Mock<IPaymentService> payment = new();
    private readonly Mock<IEventRaiser> raiser = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly ActiveCart cart = CreateCartWithItems(
        buyerId: buyerId,
        items: [
            CreateItem(forDelivery: false),
            CreateItem(forDelivery: false),
            CreateItem(forDelivery: false),
        ]
    );

    public PurchaseActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetPurchasedCartByIdQuery>(), ct))
            .ReturnsAsync(new GetPurchasedCartByIdDto(
                Id: default,
                Total: TotalCost,
                PurchaseDate: default,
                BuyerName: string.Empty,
                ShipmentId: default,
                Items: []
            ));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByBuyerIdAsync(buyerId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreatePurchasedCartCommand>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetPurchasedCartByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

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
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.IsAny<ActiveCartPurchasedDomainEvent>()
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

        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        string actual = await handler.Handle(command, ct);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartForDelivery()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(CreateCartWithItems(
                buyerId: buyerId,
                items: [
                    CreateItem(forDelivery: true),
                    CreateItem(forDelivery: false),
                    CreateItem(forDelivery: true),
                ]
            ));

        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartItemDeliveryException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByBuyerIdAsync(buyerId, false, ct))
            .ReturnsAsync(null as ActiveCart);

        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<ActiveCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
