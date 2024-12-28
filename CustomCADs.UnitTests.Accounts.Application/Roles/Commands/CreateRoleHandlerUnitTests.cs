using CustomCADs.Accounts.Application.Roles.Commands.Create;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands;

using static Constants.Roles;

public class CreateRoleHandlerUnitTests : RolesBaseUnitTests
{
    private static IEventRaiser raiser;
    private static IUnitOfWork uow;
    private static IWrites<Role> writes;

    [SetUp]
    public void SetUp()
    {
        raiser = Substitute.For<IEventRaiser>();
        uow = Substitute.For<IUnitOfWork>();
        writes = Substitute.For<IWrites<Role>>();
    }

    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
    public async Task Handler_ShouldCallDatabase(string name, string description)
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

    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
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
