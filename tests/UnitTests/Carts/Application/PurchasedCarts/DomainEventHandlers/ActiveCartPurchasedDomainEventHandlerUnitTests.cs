using CustomCADs.Carts.Application.PurchasedCarts.DomainEventHandlers;
using CustomCADs.Carts.Domain.ActiveCarts.Events;
using CustomCADs.Carts.Domain.Common;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using CustomCADs.Shared.UseCases.Products.Commands;
using CustomCADs.Shared.UseCases.Products.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.DomainEventHandlers;

using static PurchasedCartsData;

public class ActiveCartPurchasedDomainEventHandlerUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly PurchasedCartId id = ValidId1;
    private static readonly PurchasedCartItem item1 = CreateItem(forDelivery: true);
    private static readonly PurchasedCartItem item2 = CreateItem(forDelivery: false);
    private static readonly PurchasedCart cart = CreateCartWithItems(items: [item1, item2,]);

    public ActiveCartPurchasedDomainEventHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(cart);

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductPricesByIdsQuery>(), ct))
            .ReturnsAsync(new Dictionary<ProductId, decimal>()
            {
                [item1.ProductId] = item1.Price,
                [item2.ProductId] = item2.Price,
            });

        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetProductCadIdsByIdsQuery>(), ct))
            .ReturnsAsync(new Dictionary<ProductId, CadId>()
            {
                [item1.ProductId] = item1.CadId,
                [item2.ProductId] = item2.CadId,
            });

        sender.Setup(x => x.SendCommandAsync(It.IsAny<DuplicateCadsByIdsCommand>(), ct))
            .ReturnsAsync(new Dictionary<CadId, CadId>()
            {
                [item1.CadId] = item2.CadId,
                [item2.CadId] = item1.CadId,
            });
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        ActiveCartPurchasedDomainEvent de = new(
            Id: id,
            Items: []
        );
        ActiveCartPurchasedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(id, true, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPersistToDatabase()
    {
        // Arrange
        ActiveCartPurchasedDomainEvent de = new(
            Id: id,
            Items: []
        );
        ActiveCartPurchasedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        ActiveCartPurchasedDomainEvent de = new(
            Id: id,
            Items: []
        );
        ActiveCartPurchasedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Act
        await handler.Handle(de);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetProductPricesByIdsQuery>(),
        ct), Times.Once);
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetProductCadIdsByIdsQuery>(),
        ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<DuplicateCadsByIdsCommand>(),
        ct), Times.Once);
        sender.Verify(x => x.SendCommandAsync(
            It.IsAny<AddProductPurchaseCommand>(),
        ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(id, true, ct))
            .ReturnsAsync(null as PurchasedCart);

        ActiveCartPurchasedDomainEvent de = new(
            Id: id,
            Items: []
        );
        ActiveCartPurchasedDomainEventHandler handler = new(reads.Object, uow.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<PurchasedCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(de);
        });
    }
}
