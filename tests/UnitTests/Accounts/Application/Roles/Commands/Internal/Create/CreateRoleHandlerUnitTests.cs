using CustomCADs.Accounts.Application.Roles.Commands.Internal.Create;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Create;

using Data;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly CreateRoleHandler handler;
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IWrites<Role>> writes = new();

    public CreateRoleHandlerUnitTests()
    {
        handler = new(writes.Object, uow.Object, raiser.Object);
    }

    [Theory]
    [ClassData(typeof(CreateRoleValidData))]
    public async Task Handler_ShouldPersistToDatabase(RoleWriteDto dto)
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
    public async Task Handler_ShouldRaiseEvents(RoleWriteDto dto)
    {
        // Arrange
        CreateRoleCommand command = new(dto);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.Is<RoleCreatedDomainEvent>(x => x.Role.Name == dto.Name && x.Role.Description == dto.Description)
        ), Times.Once);
        raiser.Verify(x => x.RaiseApplicationEventAsync(
            It.Is<RoleCreatedApplicationEvent>(x => x.Name == dto.Name && x.Description == dto.Description)
        ), Times.Once);
    }
}
