using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Common.Caching.Roles;
using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Domain.Roles.Reads;
using CustomCADs.Shared.Application.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName;

using static CachingKeys;
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

        cache.GetRoleAsync(ValidName1).Returns(CreateRole(ValidName1, ValidDescription1));
        cache.GetRoleAsync(ValidName2).Returns(CreateRole(ValidName2, ValidDescription2));
        cache.GetRoleAsync(ValidName3).Returns(CreateRole(ValidName3, ValidDescription3));
        cache.GetRoleAsync(ValidName4).Returns(CreateRole(ValidName4, ValidDescription4));
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldCallCache_WhenCacheHit(string name)
    {
        // Arrange
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await cache.Received(1).GetRoleAsync(name);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldQueryDatabase_WhenCacheMiss(string name)
    {
        // Arrange
        cache.GetRoleAsync(name).Returns(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        await reads.Received(1).SingleByNameAsync(name, track: false, ct);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldUpdateCache_WhenDatabaseHit(string name)
    {
        // Arrange
        cache.GetRoleAsync(name).Returns(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads, cache);

        // Act
        await handler.Handle(query, ct);

        // Assert
        Role role = CreateRole(name);
        await cache.Received(1).SetAsync(
            Arg.Is<string>(key => key == $"{RoleKey}/{role.Id}"),
            Arg.Is<Role>(item => item.Name == role.Name)
        );
        await cache.Received(1).SetAsync(
            Arg.Is<string>(key => key == $"{RoleKey}/{role.Name}"),
            Arg.Is<Role>(item => item.Name == role.Name)
        );
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameHandlerValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string name)
    {
        // Arrange
        cache.GetRoleAsync(name).Returns(null as Role);
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
