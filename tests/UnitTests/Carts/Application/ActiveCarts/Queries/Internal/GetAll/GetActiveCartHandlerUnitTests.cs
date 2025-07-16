using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

using static ActiveCartsData;

public class GetActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private readonly GetActiveCartItemsHandler handler;
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	private const string Buyer = "For7a7a";
	private static readonly ActiveCartItem[] items = [
		CreateItem(),
		CreateItemWithDelivery(),
	];

	public GetActiveCartHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.AllAsync(ValidBuyerId, false, ct))
			.ReturnsAsync(items);

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		)).ReturnsAsync(Buyer);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetActiveCartItemsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(ValidBuyerId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetActiveCartItemsQuery query = new(ValidBuyerId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetUsernameByIdQuery>(x => x.Id == ValidBuyerId),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetActiveCartItemsQuery query = new(ValidBuyerId);

		// Act
		var result = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(items.Select(x => x.ProductId), result.Select(x => x.ProductId));
	}
}
