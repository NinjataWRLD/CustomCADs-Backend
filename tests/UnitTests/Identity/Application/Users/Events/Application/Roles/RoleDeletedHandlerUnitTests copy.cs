using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles;

using static UsersData;

public class RoleCreatedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RoleCreatedHandler handler;
	private readonly Mock<IRoleService> service = new();

	public RoleCreatedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RoleCreatedApplicationEvent ae = new(
			Name: ValidRole,
			Description: string.Empty
		);

		// Act
		await handler.Handle(ae);

		// Assert
		service.Verify(x => x.CreateAsync(ValidRole), Times.Once());
	}
}
