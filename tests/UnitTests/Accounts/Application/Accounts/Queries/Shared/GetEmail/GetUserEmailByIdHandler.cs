using CustomCADs.Accounts.Application.Accounts.Queries.Shared.UserEmail;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetEmail;

using static AccountsData;

public class GetUserEmailByIdHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetUserEmailByIdHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly Account account = CreateAccount();

	public GetUserEmailByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		GetUserEmailByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetUserEmailByIdQuery query = new(ValidId);

		// Act
		string email = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(account.Email, email);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);
		GetUserEmailByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
