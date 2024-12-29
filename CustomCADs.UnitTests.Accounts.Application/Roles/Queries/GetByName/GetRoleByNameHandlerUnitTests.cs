using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName;

using static RolesData;

public class GetRoleByNameHandlerData : TheoryData<string>;

public class GetRoleByNameHandlerUnitTests : RolesBaseUnitTests
{
    private readonly IRoleReads reads = Substitute.For<IRoleReads>();
    private readonly ICacheService cache = Substitute.For<ICacheService>();

    public GetRoleByNameHandlerUnitTests()
    {
        reads.SingleByNameAsync(ValidName1, track: false, ct).Returns(CreateRole(ValidName1, ValidDescription1));
        reads.SingleByNameAsync(ValidName2, track: false, ct).Returns(CreateRole(ValidName2, ValidDescription2));
        reads.SingleByNameAsync(ValidName3, track: false, ct).Returns(CreateRole(ValidName3, ValidDescription3));
        reads.SingleByNameAsync(ValidName4, track: false, ct).Returns(CreateRole(ValidName4, ValidDescription4));

        cache.GetAsync<Role>($"role/{ValidName1}").Returns(CreateRole(ValidName1, ValidDescription1));
        cache.GetAsync<Role>($"role/{ValidName2}").Returns(CreateRole(ValidName2, ValidDescription2));
        cache.GetAsync<Role>($"role/{ValidName3}").Returns(CreateRole(ValidName3, ValidDescription3));
        cache.GetAsync<Role>($"role/{ValidName4}").Returns(CreateRole(ValidName4, ValidDescription4));
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldPullFromCache_WhenCacheHit(string name)
    {
        // Assert
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await cache.Received(1).GetAsync<Role>($"roles/{name}");
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldCallDatabase_WhenCacheMiss(string name)
    {
        // Assert
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(name, track: false, ct);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string name)
    {
        // Assert
        reads.SingleByNameAsync(name, false, ct).Returns(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });

    }
}
