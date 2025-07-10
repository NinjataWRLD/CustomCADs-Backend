using CustomCADs.Identity.Application.Users.Events.Application.Roles;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Identity.Application.Users.Events.Application.Roles;

using static UsersData;

public class RoleCreatedHandlerUnitTests : UsersBaseUnitTests
{
	private readonly RoleCreatedHandler handler;
	private readonly Mock<IRoleWrites> writes = new();

	public RoleCreatedHandlerUnitTests()
	{
		handler = new(writes.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		RoleCreatedApplicationEvent ae = new(
			Name: ValidRole,
			Description: string.Empty
		);

		// Act
		await handler.Handle(ae);

		// Assert
		writes.Verify(x => x.CreateAsync(ValidRole), Times.Once());
	}
}
