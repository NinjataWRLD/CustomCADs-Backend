using CustomCADs.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;
using CustomCADs.Carts.Domain.PurchasedCarts.Entities;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Cads.Queries;

namespace CustomCADs.UnitTests.Carts.Application.PurchasedCarts.Queries.Internal.GetCadUrlGet;

using static PurchasedCartsData;

public class GetPurchasedCartCadUrlGetHandlerUnitTests : PurchasedCartsBaseUnitTests
{
	private readonly Mock<IPurchasedCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly GetPurchasedCartItemCadPresignedUrlGetHandler handler;

	private readonly PurchasedCart cart = CreateCartWithItems(
		items: [
			PurchasedCartItem.Create(
				cartId: ValidId,
				productId: CartItemsData.ValidProductId,
				cadId: CartItemsData.ValidCadId,
				customizationId: CartItemsData.ValidCustomizationId,
				price: CartItemsData.MinValidPrice,
				quantity: CartItemsData.MinValidQuantity,
				forDelivery: true,
				addedAt: DateTimeOffset.UtcNow
			),
			PurchasedCartItem.Create(
				cartId: ValidId,
				productId: CartItemsData.ValidProductId,
				cadId: CartItemsData.ValidCadId,
				customizationId: null,
				price: CartItemsData.MaxValidPrice,
				quantity: CartItemsData.MaxValidQuantity,
				forDelivery: false,
				addedAt: DateTimeOffset.UtcNow
			),
		]
	).FinishPayment(success: true);
	private const string Url = "presigned-Url";
	private const string ContentType = "presigned-Url";

	public GetPurchasedCartCadUrlGetHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(cart);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlGetByIdQuery>(x => cart.Items.Any(i => i.CadId == x.Id)),
			ct
		)).ReturnsAsync(new DownloadFileResponse(Url, ContentType));

		CoordinatesDto coords = new(0, 0, 0);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetCadCoordsByIdQuery>(x => cart.Items.Any(i => i.CadId == x.Id)),
			ct
		)).ReturnsAsync(new GetCadCoordsByIdDto(coords, coords));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			ProductId: CartItemsData.ValidProductId,
			BuyerId: ValidBuyerId
		);
		GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			ProductId: CartItemsData.ValidProductId,
			BuyerId: ValidBuyerId
		);
		GetPurchasedCartItemCadPresignedUrlGetHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetCadPresignedUrlGetByIdQuery>(x => cart.Items.Any(i => i.CadId == x.Id)),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnProperly()
	{
		// Arrange
		GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			ProductId: CartItemsData.ValidProductId,
			BuyerId: ValidBuyerId
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
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(null as PurchasedCart);

		GetPurchasedCartItemCadPresignedUrlGetQuery query = new(
			Id: ValidId,
			ProductId: CartItemsData.ValidProductId,
			BuyerId: ValidBuyerId
		);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<PurchasedCart>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
