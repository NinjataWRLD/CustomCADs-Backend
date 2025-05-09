﻿using CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;
using CustomCADs.Carts.Application.PurchasedCarts.Commands.Internal.Create;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Repositories;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;
using CustomCADs.Shared.UseCases.Customizations.Queries;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Commands.Internal.Purchase.WithDelivery;

using static ActiveCartsData;

public class PurchaseActiveCartWithDeliveryWithDeliveryHandlerUnitTests : ActiveCartsBaseUnitTests
{
    private readonly Mock<IActiveCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly Mock<IPaymentService> payment = new();
    private readonly Mock<IEventRaiser> raiser = new();
    private static readonly AccountId buyerId = ValidBuyerId1;
    private static readonly AddressDto address = new("Bulgaria", "Burgas");
    private static readonly ContactDto contact = new(null, null);

    public PurchaseActiveCartWithDeliveryWithDeliveryHandlerUnitTests()
    {
        reads.Setup(x => x.ExistsAsync(buyerId, ct))
            .ReturnsAsync(true);

        reads.Setup(x => x.AllAsync(buyerId, false, ct))
            .ReturnsAsync([
                CreateItemWithDelivery(productId: ProductId.New()),
                CreateItem(productId: ProductId.New()),
                CreateItemWithDelivery(productId: ProductId.New()),
            ]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductPricesByIdsQuery>(), ct))
            .ReturnsAsync([]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationsCostByIdsQuery>(), ct))
            .ReturnsAsync([]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationsCostByIdsQuery>(), ct))
            .ReturnsAsync([]);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCustomizationsWeightByIdsQuery>(), ct))
            .ReturnsAsync([]);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

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
        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetProductPricesByIdsQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationsCostByIdsQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetUsernameByIdQuery>()
        , ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<CreatePurchasedCartCommand>()
        , ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCustomizationsWeightByIdsQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCallPayment()
    {
        // Arrange
        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

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
        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.IsAny<ActiveCartDeliveryRequestedDomainEvent>()
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

        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

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
                CreateItem(productId: ProductId.New()),
                CreateItem(productId: ProductId.New()),
                CreateItem(productId: ProductId.New()),
            ]);

        PurchaseActiveCartWithDeliveryCommand command = new(
            PaymentMethodId: string.Empty,
            ShipmentService: string.Empty,
            BuyerId: buyerId,
            Address: address,
            Contact: contact
        );
        PurchaseActiveCartWithDeliveryHandler handler = new(reads.Object, uow.Object, sender.Object, payment.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<CustomException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
