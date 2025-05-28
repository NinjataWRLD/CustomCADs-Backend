using CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.GetAll;
using CustomCADs.Carts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Carts.Application.ActiveCarts.Queries.Internal.GetAll;

using static ActiveCartsData;

public class GetActiveCartHandlerUnitTests : ActiveCartsBaseUnitTests
{
	private const string Buyer = "For7a7a";
	private readonly Mock<IActiveCartReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private static readonly AccountId buyerId = ValidBuyerId1;

	public GetActiveCartHandlerUnitTests()
	{
		reads.Setup(x => x.AllAsync(buyerId, false, ct))
			.ReturnsAsync([]);

		sender.Setup(x => x.SendQueryAsync(It.IsAny<GetUsernameByIdQuery>(), ct))
			.ReturnsAsync(Buyer);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetActiveCartItemsQuery query = new(buyerId);
		GetActiveCartItemsHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.AllAsync(buyerId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetActiveCartItemsQuery query = new(buyerId);
		GetActiveCartItemsHandler handler = new(reads.Object, sender.Object);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.IsAny<GetUsernameByIdQuery>()
		, ct), Times.Once);
	}
}
