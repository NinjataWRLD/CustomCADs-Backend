using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.ApplicationEvents.Identity;
using CustomCADs.Shared.UseCases.Accounts.Queries;

namespace CustomCADs.UnitTests.Identity.Application.Users.Commands.Internal.ToggleViewedProductsTracking;

using static UsersData;

public class ToggleViewedProductsTrackingHandlerUnitTests : UsersBaseUnitTests
{
	private readonly ToggleViewedProductsTrackingHandler handler;
	private readonly Mock<IUserService> service = new();
	private readonly Mock<IRequestSender> sender = new();
	private readonly Mock<IEventRaiser> raiser = new();

	private const bool InitialTrackViewedProducts = false;

	public ToggleViewedProductsTrackingHandlerUnitTests()
	{
		handler = new(service.Object, sender.Object, raiser.Object);

		service.Setup(x => x.GetAccountIdAsync(MaxValidUsername)).ReturnsAsync(ValidAccountId);
		sender.Setup(x => x.SendQueryAsync(
			It.Is<GetAccountInfoByUsernameQuery>(x => x.Username == MaxValidUsername),
			ct
		)).ReturnsAsync(new AccountInfo(default, InitialTrackViewedProducts, null, null));
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		ToggleViewedProductsTrackingCommand command = new(MaxValidUsername);

		// Act
		await handler.Handle(command, ct);

		// Assert
		service.Verify(x => x.GetAccountIdAsync(MaxValidUsername), Times.Once());
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
		), Times.Once());
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
		), Times.Once());
	}
}
