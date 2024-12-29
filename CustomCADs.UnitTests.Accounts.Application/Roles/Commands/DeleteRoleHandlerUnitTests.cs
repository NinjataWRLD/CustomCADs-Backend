using CustomCADs.Accounts.Application.Roles.Commands.Delete;
using CustomCADs.Accounts.Domain.Common;
using CustomCADs.Accounts.Domain.Roles.DomainEvents;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.IntegrationEvents.Account.Roles;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Commands;

using static Constants.Roles;

public class DeleteRoleHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IEventRaiser raiser = Substitute.For<IEventRaiser>();
    private readonly IUnitOfWork uow = Substitute.For<IUnitOfWork>();
    private readonly IWrites<Role> writes = Substitute.For<IWrites<Role>>();
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();

    public DeleteRoleHandlerUnitTests()
    {
        reads.SingleByNameAsync(Name, true, ct).Returns(CreateRole());
        reads.SingleByNameAsync(Client, true, ct).Returns(CreateRole(Client, ClientDescription));
        reads.SingleByNameAsync(Contributor, true, ct).Returns(CreateRole(Contributor, ContributorDescription));
        reads.SingleByNameAsync(Designer, true, ct).Returns(CreateRole(Designer, DesignerDescription));
        reads.SingleByNameAsync(Admin, true, ct).Returns(CreateRole(Admin, AdminDescription));
    }

    [Theory]
    [InlineData(Client)]
    [InlineData(Contributor)]
    [InlineData(Designer)]
    [InlineData(Admin)]
    public async Task Handler_ShouldCallDatabase(string name)
    {
        // Arrange
        DeleteRoleCommand command = new(name);
        DeleteRoleHandler handler = new(reads, writes, uow, raiser);

        // Act
        await handler.Handle(command, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(name, track: true, ct: ct);
        writes.Received(1).Remove(Arg.Is<Role>(x => x.Name == name));
        await uow.Received(1).SaveChangesAsync(ct);
    }

    [Theory]
    [InlineData(Client)]
    [InlineData(Contributor)]
    [InlineData(Designer)]
    [InlineData(Admin)]
    public async Task Handler_ShouldRaiseEvents(string name)
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
    [InlineData(Client)]
    [InlineData(Contributor)]
    [InlineData(Designer)]
    [InlineData(Admin)]
    public async Task Handle_ShouldThrowException_WhenRoleDoesNotExists(string role)
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
