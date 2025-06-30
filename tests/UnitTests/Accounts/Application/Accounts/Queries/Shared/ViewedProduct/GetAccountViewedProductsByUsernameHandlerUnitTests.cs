using CustomCADs.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.ViewedProduct;

using static AccountsData;

public class GetAccountViewedProductsByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountViewedProductsByUsernameHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly ProductId[] expected = [];

	public GetAccountViewedProductsByUsernameHandlerUnitTests()
	{
		handler = new(reads.Object);
		reads.Setup(x => x.ViewedProductsByUsernameAsync(ValidUsername, ct))
			.ReturnsAsync(expected);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountViewedProductsByUsernameQuery query = new(ValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.ViewedProductsByUsernameAsync(ValidUsername, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAccountViewedProductsByUsernameQuery query = new(ValidUsername);

		// Act
		ProductId[] ids = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(expected, ids);
	}
}
