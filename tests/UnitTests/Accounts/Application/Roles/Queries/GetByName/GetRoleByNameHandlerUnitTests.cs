using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Queries.GetByName;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName.Data;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.GetByName;

using static CachingKeys;
using static RolesData;

public class GetRoleByNameHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<IRoleReads> reads = new();
    private readonly Mock<ICacheService> cache = new();

    public GetRoleByNameHandlerUnitTests()
    {
        reads.Setup(x => x.SingleByNameAsync(ValidName1, false, ct)).ReturnsAsync(CreateRole(ValidName1, ValidDescription1));
        reads.Setup(x => x.SingleByNameAsync(ValidName2, false, ct)).ReturnsAsync(CreateRole(ValidName2, ValidDescription2));
        reads.Setup(x => x.SingleByNameAsync(ValidName3, false, ct)).ReturnsAsync(CreateRole(ValidName3, ValidDescription3));
        reads.Setup(x => x.SingleByNameAsync(ValidName4, false, ct)).ReturnsAsync(CreateRole(ValidName4, ValidDescription4));

        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidName1}")).ReturnsAsync(CreateRole(ValidName1, ValidDescription1));
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidName2}")).ReturnsAsync(CreateRole(ValidName2, ValidDescription2));
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidName3}")).ReturnsAsync(CreateRole(ValidName3, ValidDescription3));
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{ValidName4}")).ReturnsAsync(CreateRole(ValidName4, ValidDescription4));
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameValidData))]
    public async Task Handle_ShouldCallCache_WhenCacheHit(string name)
    {
        // Arrange
        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        cache.Verify(x => x.GetAsync<Role>($"{RoleKey}/{name}"), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameValidData))]
    public async Task Handle_ShouldQueryDatabase_WhenCacheMiss(string name)
    {
        // Arrange
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.SingleByNameAsync(name, false, ct), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameValidData))]
    public async Task Handle_ShouldUpdateCache_WhenDatabaseHit(string name)
    {
        // Arrange
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        Role role = CreateRole(name);
        cache.Verify(x => x.SetAsync(
            It.Is<string>(key => key == $"{RoleKey}/{role.Id}"),
            It.Is<Role>(item => item.Name == role.Name)
        ), Times.Once);
        cache.Verify(x => x.SetAsync(
            It.Is<string>(key => key == $"{RoleKey}/{role.Name}"),
            It.Is<Role>(item => item.Name == role.Name)
        ), Times.Once);
    }

    [Theory]
    [ClassData(typeof(GetRoleByNameValidData))]
    public async Task Handle_ShouldThrowException_WhenDatabaseMiss(string name)
    {
        // Arrange
        cache.Setup(x => x.GetAsync<Role>($"{RoleKey}/{name}")).ReturnsAsync(null as Role);
        reads.Setup(x => x.SingleByNameAsync(name, false, ct)).ReturnsAsync(null as Role);

        GetRoleByNameQuery query = new(name);
        GetRoleByNameHandler handler = new(reads.Object, cache.Object);

        // Assert
        await Assert.ThrowsAsync<RoleNotFoundException>(async () =>
        {
            // Act
            await handler.Handle(query, ct);
        });
    }
}
