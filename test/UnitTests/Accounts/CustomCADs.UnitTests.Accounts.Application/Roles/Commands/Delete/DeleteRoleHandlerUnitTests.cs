using CustomCADs.Accounts.Application.Roles.Commands.Delete;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Delete.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Delete;

using static RolesData;

public class DeleteRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Role>> writes = new();
    private readonly Mock<IRoleReads> reads = new();

    public DeleteRoleHandlerUnitTests()
    {
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
        DeleteRoleHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

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
        DeleteRoleHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.Remove(It.Is<Role>(x => x.Name == name)), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handler_ShouldRaiseEvents_WhenRoleFound(string name)
    {
        // Arrange
        DeleteRoleCommand command = new(name);
        DeleteRoleHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.Is<RoleDeletedDomainEvent>(x => x.Name == name)
        ));
        raiser.Verify(x => x.RaiseIntegrationEventAsync(
            It.Is<RoleDeletedIntegrationEvent>(x => x.Name == name)
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handle_ShouldThrowException_WhenRoleNotFound(string role)
    {
        // Arrange
        reads.Setup(x => x.SingleByNameAsync(role, true, ct)).ReturnsAsync(null as Role);

        DeleteRoleCommand command = new(role);
        DeleteRoleHandler handler = new(reads.Object, writes.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
