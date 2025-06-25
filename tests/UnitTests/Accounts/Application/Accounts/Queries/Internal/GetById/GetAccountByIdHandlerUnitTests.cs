using CustomCADs.Accounts.Application.Accounts.Queries.Internal.GetById;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Internal.GetById;

using static AccountsData;

public class GetAccountByIdHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountByIdHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	public GetAccountByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(CreateAccount());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetAccountByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);
		GetAccountByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
