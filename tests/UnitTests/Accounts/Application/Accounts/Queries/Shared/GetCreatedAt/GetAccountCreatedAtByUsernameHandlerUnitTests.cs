using CustomCADs.Accounts.Application.Accounts.Queries.Shared.CreatedAt;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetCreatedAt;

using static AccountsData;

public class GetAccountCreatedAtByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountCreatedAtByUsernameHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly Account account = CreateAccount();

	public GetAccountCreatedAtByUsernameHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		GetAccountCreatedAtByUsernameQuery query = new(ValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByUsernameAsync(ValidUsername, false, ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAccountCreatedAtByUsernameQuery query = new(ValidUsername);

		// Act
		DateTimeOffset createdAt = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(account.CreatedAt, createdAt);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
	{
		// Arrange
		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct)).ReturnsAsync(null as Account);
		GetAccountCreatedAtByUsernameQuery query = new(ValidUsername);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
