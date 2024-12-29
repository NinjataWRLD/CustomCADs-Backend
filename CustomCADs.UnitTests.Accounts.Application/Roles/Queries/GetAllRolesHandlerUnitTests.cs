using CustomCADs.Accounts.Application.Roles.Queries.GetAll;
using CustomCADs.Accounts.Domain.Roles.Reads;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries;

using static Constants.Roles;

public class GetAllRolesHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();
    private readonly Role[] roles = Role.CreateRange(
        (1, Client, ClientDescription),
        (2, Contributor, ContributorDescription),
        (3, Designer, DesignerDescription),
        (4, Admin, AdminDescription)
    ).ToArray();

    public GetAllRolesHandlerUnitTests()
    {
        reads.AllAsync(track: false, ct).Returns(roles);
    }

    [Fact]
    public async Task Handle_ShouldCallDatabase()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(track: false, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads);

        // Act
        IEnumerable<RoleReadDto> roles = await handler.Handle(query, ct);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.Equal(roles.Select(r => r.Id), this.roles.Select(r => r.Id));
            Assert.Equal(roles.Select(r => r.Name), this.roles.Select(r => r.Name));
            Assert.Equal(roles.Select(r => r.Description), this.roles.Select(r => r.Description));
        });
    }
}
