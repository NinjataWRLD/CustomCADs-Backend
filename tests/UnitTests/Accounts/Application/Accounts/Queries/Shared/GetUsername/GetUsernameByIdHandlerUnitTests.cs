using CustomCADs.Accounts.Application.Accounts.Queries.Shared.Username;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Application.Exceptions;
using CustomCADs.Shared.Application.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Queries.Shared.GetUsername;

using static AccountsData;

public class GetUsernameByIdHandlerUnitTests : AccountsBaseUnitTests
{
	private readonly GetUsernameByIdHandler handler;
	private readonly Mock<IAccountReads> reads = new();

	public GetUsernameByIdHandlerUnitTests()
	{
		handler = new(reads.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(CreateAccount());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetUsernameByIdQuery query = new(ValidId);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, false, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldReturnResult()
	{
		// Arrange
		GetUsernameByIdQuery query = new(ValidId);

		// Act
		string actualUsername = await handler.Handle(query, ct);

		// Assert
		Assert.Equal(ValidUsername, actualUsername);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenAccountNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, false, ct)).ReturnsAsync(null as Account);
		GetUsernameByIdQuery query = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Account>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
