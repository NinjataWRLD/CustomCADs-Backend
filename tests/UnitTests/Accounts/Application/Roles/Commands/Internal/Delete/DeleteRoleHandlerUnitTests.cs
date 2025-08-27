using CustomCADs.Accounts.Application.Roles.Commands.Internal.Delete;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Repositories.Writes;
using CustomCADs.Shared.Application.Abstractions.Cache;
using CustomCADs.Shared.Application.Abstractions.Events;
using CustomCADs.Shared.Application.Events.Account.Roles;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Delete;

using static RolesData;

public class DeleteRoleHandlerUnitTests : RolesBaseUnitTests
{
	private readonly DeleteRoleHandler handler;
	private readonly Mock<IEventRaiser> raiser = new();
	private readonly Mock<BaseCachingService<RoleId, Role>> cache = new();
	private readonly Mock<IUnitOfWork> uow = new();
	private readonly Mock<IRoleWrites> writes = new();
	private readonly Mock<IRoleReads> reads = new();

	public DeleteRoleHandlerUnitTests()
	{
		handler = new(reads.Object, writes.Object, uow.Object, cache.Object, raiser.Object);

		reads.Setup(x => x.SingleByIdAsync(ValidId, true, ct)).ReturnsAsync(CreateRole());
	}

	[Fact]
	public async Task Handle_ShouldQueryDatabase()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		reads.Verify(x => x.SingleByIdAsync(ValidId, true, ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldPersistToDatabase()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		writes.Verify(x => x.Remove(
			It.Is<Role>(x => x.Id == ValidId)
		), Times.Once());
		uow.Verify(x => x.SaveChangesAsync(ct), Times.Once());
	}

	[Fact]
	public async Task Handle_ShouldRaiseEvents()
	{
		// Arrange
		DeleteRoleCommand command = new(ValidId);

		// Act
		await handler.Handle(command, ct);

		// Assert
		raiser.Verify(x => x.RaiseApplicationEventAsync(
			It.Is<RoleDeletedApplicationEvent>(x => x.Name == ValidName)
		), Times.Once());
	}
}
