using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Create;

public class CreateRoleHandlerData : TheoryData<string, string>;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IWrites<Role> writes = Substitute.For<IWrites<Role>>();

    [Theory]
    [ClassData(typeof(EditRoleHandlerValidData))]
    public async Task Handler_ShouldPersistToDatabase(string name, string description)
    {
        // Arrange
        RoleWriteDto dto = new(name, description);
        CreateRoleCommand command = new(dto);
        CreateRoleHandler handler = new(writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await writes.Received(1).AddAsync(
            Arg.Is<Role>(x => x.Name == name && x.Description == description),
            ct: ct
        );
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [ClassData(typeof(EditRoleHandlerValidData))]
    public async Task Handler_ShouldRaiseEvents(string name, string description)
    {
        // Arrange
        RoleWriteDto dto = new(name, description);
        CreateRoleCommand command = new(dto);
        CreateRoleHandler handler = new(writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await raiser.Received(1).RaiseDomainEventAsync(
            Arg.Is<RoleCreatedDomainEvent>(x => x.Role.Name == name && x.Role.Description == description)
        );
        await raiser.Received(1).RaiseIntegrationEventAsync(
            Arg.Is<RoleCreatedIntegrationEvent>(x => x.Name == name && x.Description == description)
        );
    }
}
