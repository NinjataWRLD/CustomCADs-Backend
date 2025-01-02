using CustomCADs.Accounts.Application.Roles.Commands.Edit;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Edit;

using static RolesData;

public class EditRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();

    public EditRoleHandlerUnitTests()
    {
        reads.SingleByNameAsync(ValidName1, true, ct).Returns(CreateRole());
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldQueryDatabase(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(ValidName1, track: true, ct: ct);
    }
    
    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldPersistToDatabase_WhenRoleFound(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldRaiseEvents_WhenRoleFound(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await raiser.Received(1).RaiseDomainEventAsync(
            Arg.Is<RoleEditedDomainEvent>(x => x.Role.Name == name && x.Role.Description == description)
        );
        await raiser.Received(1).RaiseIntegrationEventAsync(
            Arg.Is<RoleEditedIntegrationEvent>(x => x.Name == name && x.Description == description)
        );
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldModifyRole_WhenRoleFound(string name, string description)
    {
        Role role = CreateRole();
        reads.SingleByNameAsync(ValidName1, true, ct).Returns(role);

        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.Equal(role.Name, name);
            Assert.Equal(role.Description, description);
        });
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handle_ShouldThrowException_WhenRoleNotFound(string name, string description)
    {
        // Arrange
        string role = ValidName1;
        reads.SingleByNameAsync(role, true, ct).Returns(null as Role);

        EditRoleCommand command = new(role, new(name, description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
