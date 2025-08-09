using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles;

using static UsersData;

public class RoleDeletedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RoleDeletedHandler handler;
	private readonly Mock<IRoleService> service = new();

	public RoleDeletedHandlerUnitTests()
	{
		handler = new(service.Object);
	}

	[Fact]
	public async Task Handle_ShouldCallService()
	{
		// Arrange
		RoleDeletedApplicationEvent ae = new(ValidRole);

		// Act
		await handler.Handle(ae);

		// Assert
		service.Verify(x => x.DeleteAsync(ValidRole), Times.Once());
	}
}
