using CustomCADs.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.PurchasedCarts.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Carts;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.GetCadUrlGet;

using static PurchasedCartsData;

public class GetPurchasedCartCadUrlGetHandlerUnitTests : PurchasedCartsBaseUnitTests
{
    private readonly Mock<IPurchasedCartReads> reads = new();
    private readonly Mock<IRequestSender> sender = new();
    private readonly PurchasedCart cart = CreateCartWithItems(
        items: [
            PurchasedCartItem.Create(
                ValidId1,
                CartItemsData.ValidProductId1,
                CartItemsData.ValidCadId1,
                CartItemsData.ValidPrice1,
                CartItemsData.ValidQuantity1,
                true
            ),
            PurchasedCartItem.Create(
                ValidId2,
                CartItemsData.ValidProductId2,
                CartItemsData.ValidCadId2,
                CartItemsData.ValidPrice2,
                CartItemsData.ValidQuantity2,
                false
            ),
        ]
    );
    private static readonly PurchasedCartId cartId = ValidId1;
    private static readonly PurchasedCartItemId itemId = PurchasedCartItemId.New(Guid.Empty);
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
            ItemId: itemId,
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
            ItemId: itemId,
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
            ItemId: itemId,
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
            ItemId: itemId,
            BuyerId: buyerId
        );
        GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

        // Assert
        await Assert.ThrowsAsync<PurchasedCartNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
