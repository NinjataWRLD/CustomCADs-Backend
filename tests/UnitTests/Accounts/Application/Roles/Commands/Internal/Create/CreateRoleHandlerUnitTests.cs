using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using Data;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
	private readonly CreateRoleHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();

	public CreateRoleHandlerUnitTests()
	{
		handler = new(writes.Object, uow.Object, cache.Object, raiser.Object);
	}

	[Theory]
	[ClassData(typeof(CreateRoleValidData))]
	public async Task Handle_ShouldPersistToDatabase(RoleWriteDto dto)
	{
		// Arrange
		CreateRoleCommand command = new(dto);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.AddAsync(
			It.Is<Role>(x => x.Name == dto.Name && x.Description == dto.Description),
			ct
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateRoleValidData))]
	public async Task Handle_ShouldUpdateCache(RoleWriteDto dto)
	{
		// Arrange
		CreateRoleCommand command = new(dto);

		// Act
		await handler.Handle(command, ct);

		// Assert
		cache.Verify(x => x.UpdateAsync(
			It.IsAny<RoleId>(),
			It.Is<Role>(x => x.Name == dto.Name && x.Description == dto.Description)
		), Times.Once);
	}

	[Theory]
	[ClassData(typeof(CreateRoleValidData))]
	public async Task Handle_ShouldRaiseEvents(RoleWriteDto dto)
	{
		// Arrange
		CreateRoleCommand command = new(dto);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleCreatedApplicationEvent>(x => x.Name == dto.Name && x.Description == dto.Description)
		), Times.Once);
	}
}
