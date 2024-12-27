using CustomCADs.Accounts.Application.Roles.Queries.GetAll;
using CustomCADs.Accounts.Domain.Roles.Reads;

namespace CustomCADs.Accounts.UnitTests.Application.Accounts.Queries;

using static Constants.Roles;

public class GetAllAccountsHandlerUnitTests : AccountsBaseUnitTests
{
    private static IRoleReads reads;
    private readonly Role[] roles = Role.CreateRange(
        (1, Client, ClientDescription),
        (2, Contributor, ContributorDescription),
        (3, Designer, DesignerDescription),
        (4, Admin, AdminDescription)
    ).ToArray();

    [SetUp]
    public void Setup()
    {
        reads = Substitute.For<IRoleReads>();
        reads.AllAsync(track: false, ct).Returns(roles);
    }

    [Test]
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

    [Test]
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
            Assert.That(roles.Select(r => r.Id), Is.EqualTo(this.roles.Select(r => r.Id)));
            Assert.That(roles.Select(r => r.Name), Is.EqualTo(this.roles.Select(r => r.Name)));
            Assert.That(roles.Select(r => r.Description), Is.EqualTo(this.roles.Select(r => r.Description)));
        });
    }
}
