﻿using CustomCADs.Accounts.Application.Accounts.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.IntegrationEvents.Account.Accounts;
using CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create.Data;
using System.Xml.Linq;

namespace CustomCADs.UnitTests.Accounts.Application.Accounts.Commands.Create;

public class CreateAccountHandlerUnitTests : AccountsBaseUnitTests
{
    private readonly Mock<IWrites<Account>> writes = new();
    private readonly Mock<IUnitOfWork> uow = new();
    private readonly Mock<IEventRaiser> raiser = new();


    [Theory]
    [ClassData(typeof(CreateAccountValidData))]
    public async Task Handle_ShouldPersistToDatabase(string role, string username, string email, string timeZone, string password, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            Password: password,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes.Object, uow.Object, raiser.Object);

        // Act
        await handler.Handle(command, ct);

        // Assert
        writes.Verify(x => x.AddAsync(
            It.Is<Account>(x =>
                x.RoleName == role
                && x.Username == username
                && x.Email == email
                && x.TimeZone == timeZone
                && x.FirstName == firstName
                && x.LastName == lastName
            ),
            ct
        ), Times.Once);
        uow.Verify(x => x.SaveChangesAsync(ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(CreateAccountValidData))]
    public async Task Handle_ShouldRaiseEvents(string role, string username, string email, string timeZone, string password, string? firstName, string? lastName)
    {
        // Arrange
        CreateAccountCommand command = new(
            Role: role,
            Username: username,
            Email: email,
            TimeZone: timeZone,
            Password: password,
            FirstName: firstName,
            LastName: lastName
        );
        CreateAccountHandler handler = new(writes.Object, uow.Object, raiser.Object);

        // Act
        AccountId id = await handler.Handle(command, CancellationToken.None);

        // Assert
        raiser.Verify(x => x.RaiseIntegrationEventAsync(
            It.Is<AccountCreatedIntegrationEvent>(x =>
                x.Id == id
                && x.Username == username
                && x.Email == email
                && x.Password == password
            )
        ),Times.Once);
    }
}
