using CustomCADs.Accounts.Application.Roles.Commands.Delete;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Delete.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Delete;

using static RolesData;

public class DeleteRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IWrites<Role> writes = Substitute.For<IWrites<Role>>();
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();

    public DeleteRoleHandlerUnitTests()
    {
        reads.SingleByNameAsync(ValidName1, true, ct)
            .Returns(CreateRole(ValidName1, ValidDescription1));

        reads.SingleByNameAsync(ValidName2, true, ct)
            .Returns(CreateRole(ValidName2, ValidDescription2));

        reads.SingleByNameAsync(ValidName3, true, ct)
            .Returns(CreateRole(ValidName3, ValidDescription3));

        reads.SingleByNameAsync(ValidName4, true, ct)
            .Returns(CreateRole(ValidName4, ValidDescription4));
    }

    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handler_ShouldQueryDatabase(string name)
    {
        // Arrange
        DeleteRoleCommand command = new(name);
        DeleteRoleHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(name, track: true, ct: ct);
    }
    
    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handler_ShouldPersistToDatabase_WhenRoleFound(string name)
    {
        // Arrange
        DeleteRoleCommand command = new(name);
        DeleteRoleHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Received(1).Remove(Arg.Is<Role>(x => x.Name == name));
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handler_ShouldRaiseEvents_WhenRoleFound(string name)
    {
        // Arrange
        DeleteRoleCommand command = new(name);
        DeleteRoleHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await raiser.Received(1).RaiseDomainEventAsync(
            Arg.Is<RoleDeletedDomainEvent>(x => x.Name == name)
        );
        await raiser.Received(1).RaiseIntegrationEventAsync(
            Arg.Is<RoleDeletedIntegrationEvent>(x => x.Name == name)
        );
    }

    [Theory]
    [ClassData(typeof(DeleteRoleValidData))]
    public async Task Handle_ShouldThrowException_WhenRoleNotFound(string role)
    {
        // Arrange
        reads.SingleByNameAsync(role, true, ct).Returns(null as Role);

        DeleteRoleCommand command = new(role);
        DeleteRoleHandler handler = new(reads, writes, uow, raiser);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
