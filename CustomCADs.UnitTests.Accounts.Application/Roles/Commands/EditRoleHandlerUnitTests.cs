using CustomCADs.Accounts.Application.Roles.Commands.Edit;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands;

using static Constants.Roles;

public class EditRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();

    public EditRoleHandlerUnitTests()
    {
        reads.SingleByNameAsync(Name, true, ct).Returns(CreateRole());
    }

    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
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
            Assert.Equal(role.Name, name);
            Assert.Equal(role.Description, description);
        });
    }

    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
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

    [Theory]
    [InlineData(Client, ClientDescription)]
    [InlineData(Contributor, ContributorDescription)]
    [InlineData(Designer, DesignerDescription)]
    [InlineData(Admin, AdminDescription)]
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

    [Theory]
    [InlineData(Client)]
    [InlineData(Contributor)]
    [InlineData(Designer)]
    [InlineData(Admin)]
    public async Task Handle_ShouldThrowException_WhenRoleDoesNotExists(string role)
    {
        // Arrange
        reads.SingleByNameAsync(role, true, ct).Returns(null as Role);

        EditRoleCommand command = new(role, new(Name, Description));
        EditRoleHandler handler = new(reads, uow, raiser);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(command, ct);
        });
    }
}
