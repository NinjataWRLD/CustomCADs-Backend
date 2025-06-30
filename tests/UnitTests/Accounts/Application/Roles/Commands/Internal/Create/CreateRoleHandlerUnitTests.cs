using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using static RolesData;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
	private readonly CreateRoleHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();

	private static readonly RoleWriteDto role = new(ValidName, ValidDescription);

	public CreateRoleHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object, raiser.Object);
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		CreateRoleCommand command = new(role);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Role>(x => x.Name == role.Name && x.Description == role.Description),
			ct
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldUpdateCache()
	{
		// Arrange
		CreateRoleCommand command = new(role);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(
			It.IsAny<RoleId>(),
			It.Is<Role>(x => x.Name == role.Name && x.Description == role.Description)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		CreateRoleCommand command = new(role);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleCreatedApplicationEvent>(x => x.Name == role.Name && x.Description == role.Description)
		), Times.Once);
	}
}
