using CustomCADs.Identity.Application.Users.Queries.Internal.GetByUsername;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Queries.Internal.GetByUsername;

using static UsersData;

public class GetUserByUsernameHandlerUnitTests : UsersBaseUnitTests
{
	private readonly GetUserByUsernameHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();

	public GetUserByUsernameHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object);

		reads.Setup(x => x.GetByUsernameAsync(MaxValidUsername))
			.ReturnsAsync(CreateUserWithId());

		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(new AccountInfo(DateTimeOffset.UtcNow, true, null, null));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		GetUserByUsernameQuery query = new(MaxValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		GetUserByUsernameQuery query = new(MaxValidUsername);

		// Act
		await handler.Handle(query, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		), Times.Once());
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountViewedProductsByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(MaxValidUsername)).ReturnsAsync(null as User);
		GetUserByUsernameQuery query = new(MaxValidUsername);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(query, ct)
		);
	}
}
