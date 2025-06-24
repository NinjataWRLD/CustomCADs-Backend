using CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;
using CustomCADs.Shared.Core.Common.Exceptions.Application;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Delete;

using Data;
using static RolesData;

public class DeleteRoleHandlerUnitTests : RolesBaseUnitTests
{
	private readonly DeleteRoleHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();
	private readonly Mock<IRoleReads> reads = new();

	public DeleteRoleHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(CreateRole(ValidName, ValidDescription));
	}

	[Fact]
	public async Task Handler_ShouldQueryDatabase()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once);
	}

	[Fact]
	public async Task Handler_ShouldPersistToDatabase_WhenRoleFound()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(
			It.Is<Role>(x => x.Id == ValidId)
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Fact]
	public async Task Handler_ShouldRaiseEvents_WhenRoleFound()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.Is<RoleDeletedDomainEvent>(x => x.Id == ValidId)
		));
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleDeletedApplicationEvent>(x => x.Name == ValidName)
		), Times.Once);
	}

	[Fact]
	public async Task Handle_ShouldThrowException_WhenRoleNotFound()
	{
		// Arrange
		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(null as Role);
		DeleteRoleCommand command = new(ValidId);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Role>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
