using CustomCADs.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles;

using static UsersData;

public class RoleDeletedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RoleDeletedHandler handler;
	private readonly Mock<IRoleWrites> writes = new();

	public RoleDeletedHandlerUnitTests()
	{
		handler = new(writes.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RoleDeletedApplicationEvent ae = new(ValidRole);

		// Act
		await handler.Handle(ae);

		// Assert
		writes.Verify(x => x.DeleteAsync(ValidRole), Times.Once());
	}
}
