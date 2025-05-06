using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

using static ActiveCartsData;

public class PurchaseActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Mock<IPaymentService> payment = new();
    private static readonly AccountId buyerId = ValidBuyerId1;

    public PurchaseActiveCartHandlerUnitTests()
    {
        reads.Setup(x => x.ExistsAsync(buyerId, ct))
            .ReturnsAsync(true);

        reads.Setup(x => x.AllAsync(buyerId, false, ct))
            .ReturnsAsync([
                CreateItem(productId: ProductId.New()),
                CreateItem(productId: ProductId.New()),
                CreateItem(productId: ProductId.New()),
            ]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductPricesByIdsQuery>(), ct))
            .ReturnsAsync([]);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.ExistsAsync(buyerId, ct), Times.Once);
        reads.Verify(x => x.AllAsync(buyerId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetProductPricesByIdsQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreatePurchasedCartCommand>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

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
        PaymentDto expected = new(string.Empty, Message: "Payment Status Message");
        payment.Setup(x => x.InitializePayment(
            It.IsAny<string>(),
            It.IsAny<decimal>(),
            It.IsAny<string>(),
            ct
        )).ReturnsAsync(expected);

        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

        // Act
        PaymentDto actual = await handler.Handle(command, ct);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartForDelivery()
    {
        // Arrange
        reads.Setup(x => x.AllAsync(buyerId, false, ct))
            .ReturnsAsync([
                CreateItemWithDelivery(),
                CreateItem(),
                CreateItemWithDelivery(),
            ]);

        PurchaseActiveCartCommand command = new(string.Empty, buyerId);
        PurchaseActiveCartHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
