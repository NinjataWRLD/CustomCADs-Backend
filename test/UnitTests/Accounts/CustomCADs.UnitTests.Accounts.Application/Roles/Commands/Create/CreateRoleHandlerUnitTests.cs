using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Role>> writes = new();

    [Theory]
    [ClassData(typeof(CreateRoleValidData))]
    public async Task Handler_ShouldPersistToDatabase(string name, string description)
    {
        // Arrange
        RoleWriteDto dto = new(name, description);
        CreateRoleCommand command = new(dto);
        CreateRoleHandler handler = new(writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Role>(x => x.Name == name && x.Description == description),
            ct
        ), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateRoleValidData))]
    public async Task Handler_ShouldRaiseEvents(string name, string description)
    {
        // Arrange
        RoleWriteDto dto = new(name, description);
        CreateRoleCommand command = new(dto);
        CreateRoleHandler handler = new(writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.Is<RoleCreatedDomainEvent>(x => x.Role.Name == name && x.Role.Description == description)
        ), Times.Once);
        raiser.Verify(x => x.RaiseIntegrationEventAsync(
            It.Is<RoleCreatedIntegrationEvent>(x => x.Name == name && x.Description == description)
        ), Times.Once);
    }
}
