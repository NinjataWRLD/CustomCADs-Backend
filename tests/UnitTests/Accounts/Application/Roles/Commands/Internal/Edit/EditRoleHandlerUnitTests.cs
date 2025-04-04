﻿using CustomCADs.Accounts.Application.Roles.Commands.Internal.Edit;
using CustomCADs.Accounts.Domain.Repositories;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Accounts.Domain.Roles.Events;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.ApplicationEvents.Account.Roles;
using CustomCADs.Shared.Core.Common.Exceptions.Application;
using CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands.Internal.Edit;

using static RolesData;

public class EditRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<IEventRaiser> raiser = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IRoleReads> reads = new();

    public EditRoleHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByNameAsync(ValidName1, true, ct))
            .ReturnsAsync(CreateRole());
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldQueryDatabase(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        reads.Verify(x => x.SingleByNameAsync(ValidName1, true, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldPersistToDatabase_WhenRoleFound(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldRaiseEvents_WhenRoleFound(string name, string description)
    {
        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        raiser.Verify(x => x.RaiseDomainEventAsync(
            It.Is<RoleEditedDomainEvent>(x => x.Role.Name == name && x.Role.Description == description)
        ), Times.Once);
        raiser.Verify(x => x.RaiseApplicationEventAsync(
            It.Is<RoleEditedApplicationEvent>(x => x.Name == name && x.Description == description)
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(EditRoleValidData))]
    public async Task Handler_ShouldModifyRole_WhenRoleFound(string name, string description)
    {
        Role role = CreateRole();
        reads.Setup(x => x.SingleByNameAsync(ValidName1, true, ct)).ReturnsAsync(role);

        // Arrange
        EditRoleCommand command = new(ValidName1, new(name, description));
        EditRoleHandler handler = new(reads.Object, uow.Object, raiser.Object);

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
        reads.Setup(x => x.SingleByNameAsync(role, true, ct)).ReturnsAsync(null as Role);

        EditRoleCommand command = new(role, new(name, description));
        EditRoleHandler handler = new(reads.Object, uow.Object, raiser.Object);

        // Assert
        await Assert.ThrowsAsync<CustomNotFoundException<Role>>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
