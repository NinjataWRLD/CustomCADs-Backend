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

		reads.Setup(x => x.SingleByNameAsync(ValidName1, true, ct)).ReturnsAsync(CreateRole(ValidName1, ValidDescription1));
		reads.Setup(x => x.SingleByNameAsync(ValidName2, true, ct)).ReturnsAsync(CreateRole(ValidName2, ValidDescription2));
		reads.Setup(x => x.SingleByNameAsync(ValidName3, true, ct)).ReturnsAsync(CreateRole(ValidName3, ValidDescription3));
		reads.Setup(x => x.SingleByNameAsync(ValidName4, true, ct)).ReturnsAsync(CreateRole(ValidName4, ValidDescription4));
	}

	[Theory]
	[ClassData(typeof(DeleteRoleValidData))]
	public async Task Handler_ShouldQueryDatabase(string name)
	{
		// Arrange
		DeleteRoleCommand command = new(name);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByNameAsync(name, true, ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(DeleteRoleValidData))]
	public async Task Handler_ShouldPersistToDatabase_WhenRoleFound(string name)
	{
		// Arrange
		DeleteRoleCommand command = new(name);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(
			It.Is<Role>(x => x.Name == name)
		), Times.Once);
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
	}

	[Theory]
	[ClassData(typeof(DeleteRoleValidData))]
	public async Task Handler_ShouldRaiseEvents_WhenRoleFound(string name)
	{
		// Arrange
		DeleteRoleCommand command = new(name);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseDomainEventAsync(
			It.Is<RoleDeletedDomainEvent>(x => x.Name == name)
		));
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleDeletedApplicationEvent>(x => x.Name == name)
		), Times.Once);
	}

	[Theory]
	[ClassData(typeof(DeleteRoleValidData))]
	public async Task Handle_ShouldThrowException_WhenRoleNotFound(string role)
	{
		// Arrange
		reads.Setup(x => x.SingleByNameAsync(role, true, ct)).ReturnsAsync(null as Role);
		DeleteRoleCommand command = new(role);

		// Assert
		await Assert.ThrowsAsync<CustomNotFoundException<Role>>(
			// Act
			async () => await handler.Handle(command, ct)
		);
	}
}
