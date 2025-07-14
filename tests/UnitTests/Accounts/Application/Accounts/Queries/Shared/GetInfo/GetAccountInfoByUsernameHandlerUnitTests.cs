using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Info;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetInfo;

using static AccountsData;

public class GetAccountInfoByUsernameHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetAccountInfoByUsernameHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	private static readonly Account account = CreateAccount();

	public GetAccountInfoByUsernameHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct))
			.ReturnsAsync(account);
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		GetAccountInfoByUsernameQuery query = new(ValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByUsernameAsync(ValidUsername, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetAccountInfoByUsernameQuery query = new(ValidUsername);

		// Act
		AccountInfo info = await handler.Handle(query, ct);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(account.CreatedAt, info.CreatedAt),
			() => Assert.Equal(account.TrackViewedProducts, info.TrackViewedProducts),
			() => Assert.Equal(account.FirstName, info.FirstName),
			() => Assert.Equal(account.LastName, info.LastName)
		);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountDoesNotExist()
	{
		// Arrange
		reads.Setup(x => x.SingleByUsernameAsync(ValidUsername, false, ct)).ReturnsAsync(null as Account);
		GetAccountInfoByUsernameQuery query = new(ValidUsername);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
