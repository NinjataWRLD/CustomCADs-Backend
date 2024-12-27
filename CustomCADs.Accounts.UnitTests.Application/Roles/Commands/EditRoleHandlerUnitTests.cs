using CustomCADs.Accounts.Application.Roles.Commands.Delete;
using CustomCADs.Accounts.Application.Roles.Commands.Edit;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.Accounts.UnitTests.Application.Roles.Commands;

using static Constants.Roles;

public class EditRoleHandlerUnitTests : RolesBaseUnitTests
{
    private static IEventRaiser raiser;
    private static IUnitOfWork uow;
    private static IRoleReads reads;

    [SetUp]
    public void SetUp()
    {
        raiser = Substitute.For<IEventRaiser>();
        uow = Substitute.For<IUnitOfWork>();
        reads = Substitute.For<IRoleReads>();
        reads.SingleByNameAsync(Name, true, ct).Returns(CreateRole());
    }

    [Test]
    [TestCase(Client, ClientDescription)]
    [TestCase(Contributor, ContributorDescription)]
    [TestCase(Designer, DesignerDescription)]
    [TestCase(Admin, AdminDescription)]
    public async Task Handler_ShouldModifyRole(string name, string description)
    {
        Role role = CreateRole();
        reads.SingleByNameAsync(Name, true, ct).Returns(role);

        // Arrange
        RoleWriteDto dto = new(name, description);
        EditRoleCommand command = new(Name, dto);
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(role.Name, Is.EqualTo(name));
            Assert.That(role.Description, Is.EqualTo(description));
        });
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
        EditRoleCommand command = new(Name, dto);
        EditRoleHandler handler = new(reads, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(Name, track: true, ct: ct);
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
        EditRoleCommand command = new(Name, dto);
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

    [Test]
    [TestCase(Client)]
    [TestCase(Contributor)]
    [TestCase(Designer)]
    [TestCase(Admin)]
    public void Handle_ShouldThrowException_WhenRoleDoesNotExists(string role)
    {
        // Arrange
        reads.SingleByNameAsync(role, true, ct).Returns(null as Role);

        EditRoleCommand command = new(role, new(Name, Description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Assert
        Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
