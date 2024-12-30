using CustomCADs.Accounts.Application.Common.Caching.Roles;
using CustomCADs.Accounts.Application.Roles.Queries.GetAll;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetAll;

using static Constants.Roles;

public class GetAllRolesHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();
    private readonly ICacheService cache = Substitute.For<ICacheService>();
    private readonly Role[] roles = Role.CreateRange(
        (1, Client, ClientDescription),
        (2, Contributor, ContributorDescription),
        (3, Designer, DesignerDescription),
        (4, Admin, AdminDescription)
    ).ToArray();

    public GetAllRolesHandlerUnitTests()
    {
        cache.GetRolesArrayAsync().Returns(roles);
        reads.AllAsync(track: false, ct).Returns(roles);
    }

    [Fact]
    public async Task Handle_ShouldCallCache_OnCacheHit()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await cache.Received(1).GetRolesArrayAsync();
    }
    
    [Fact]
    public async Task Handle_ShouldCallDatabase_OnCacheMiss()
    {
        // Assert
        cache.GetRolesArrayAsync().Returns(null as Role[]);

        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).AllAsync(track: false, ct);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_OnCacheHit()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads, cache);

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

    [Fact]
    public async Task Handle_ShouldReturnResult_OnCacheMiss()
    {
        // Assert
        cache.GetRolesArrayAsync().Returns(null as Role[]);
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads, cache);

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
