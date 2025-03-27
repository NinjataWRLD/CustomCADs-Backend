using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

using static PurchasedCartsData;

public class GetPurchasedCartCadUrlGetHandlerUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly PurchasedCart cart = CreateCartWithItems(
        items: [
            PurchasedCartItem.Create(
                cartId: ValidId1,
                productId: CartItemsData.ValidProductId1,
                cadId: CartItemsData.ValidCadId1,
                customizationId: CartItemsData.ValidCustomizationId1,
                price: CartItemsData.ValidPrice1,
                quantity: CartItemsData.ValidQuantity1,
                forDelivery: true
            ),
            PurchasedCartItem.Create(
                cartId: ValidId2,
                productId: CartItemsData.ValidProductId2,
                cadId: CartItemsData.ValidCadId2,
                customizationId: null,
                price: CartItemsData.ValidPrice2,
                quantity: CartItemsData.ValidQuantity2,
                forDelivery: false
            ),
        ]
    );
    private static readonly PurchasedCartId cartId = ValidId1;
    private static readonly AccountId buyerId = ValidBuyerId1;
    private const string Url = "presigned-Url";
    private const string ContentType = "presigned-Url";

    public GetPurchasedCartCadUrlGetHandlerUnitTests()
    {
        sender.Setup(x => x.SendQueryAsync(It.IsAny<GetCadPresignedUrlGetByIdQuery>(), ct))
            .ReturnsAsync((Url, ContentType));
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(cartId, false, ct))
            .ReturnsAsync(cart);

        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: cartId,
            ProductId: CartItemsData.ValidProductId1,
            BuyerId: buyerId
        );
        GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByIdAsync(cartId, false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSendRequests()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(cartId, false, ct))
            .ReturnsAsync(cart);

        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: cartId,
            ProductId: CartItemsData.ValidProductId1,
            BuyerId: buyerId
        );
        GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        sender.Verify(x => x.SendQueryAsync(
            It.IsAny<GetCadPresignedUrlGetByIdQuery>()
        , ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnProperly()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(cartId, false, ct))
            .ReturnsAsync(cart);

        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: cartId,
            ProductId: CartItemsData.ValidProductId1,
            BuyerId: buyerId
        );
        GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Act
        var result = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(
            () => Assert.Equal(Url, result.PresignedUrl),
            () => Assert.Equal(ContentType, result.ContentType)
        );
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenPurchasedCartNotFound()
    {
        // Arrange
        reads.Setup(x => x.SingleByIdAsync(cartId, false, ct))
            .ReturnsAsync(null as PurchasedCart);

        GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
            Id: cartId,
            ProductId: CartItemsData.ValidProductId1,
            BuyerId: buyerId
        );
        GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
