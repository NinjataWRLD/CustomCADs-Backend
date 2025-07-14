using CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;
using CustomCADs.Identity.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

using static UsersData;

public class ToggleViewedProductsTrackingHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ToggleViewedProductsTrackingHandler handler;
	private readonly Mock<IUserReads> reads = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const bool InitialTrackViewedProducts = false;

	public ToggleViewedProductsTrackingHandlerUnitTests()
	{
		handler = new(reads.Object, sender.Object, raiser.Object);

		reads.Setup(x => x.GetByUsernameAsync(MaxValidUsername))
			.ReturnsAsync(CreateUser());
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(new AccountInfo(default, InitialTrackViewedProducts, null, null));
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		ToggleViewedProductsTrackingCommand command = new(MaxValidUsername);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.GetByUsernameAsync(MaxValidUsername), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldSendRequests()
	{
		// Arrange
		ToggleViewedProductsTrackingCommand command = new(MaxValidUsername);

		// Act
		await handler.Handle(command, ct);

		// Assert
		sender.Verify(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		ToggleViewedProductsTrackingCommand command = new(MaxValidUsername);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<UserEditedApplicationEvent>(x =>
				x.TrackViewedProducts == !InitialTrackViewedProducts
				&& x.Id == ValidAccountId
			)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenUserNotFound()
	{
		// Arrange
		reads.Setup(x => x.GetByUsernameAsync(MaxValidUsername)).ReturnsAsync(null as User);
		ToggleViewedProductsTrackingCommand command = new(MaxValidUsername);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<User>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
