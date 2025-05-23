﻿using CustomCADs.Accounts.Application.Common.Caching;
using CustomCADs.Accounts.Application.Roles.Queries.Internal.GetAll;
using CustomCADs.Accounts.Domain.Repositories.Reads;
using CustomCADs.Shared.Abstractions.Cache;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetAll;

using static CachingKeys;
using static Constants.Roles;

public class GetAllRolesHandlerUnitTests : RolesBaseUnitTests
{
    private readonly Mock<IRoleReads> reads = new();
    private readonly Mock<ICacheService> cache = new();
    private readonly Role[] roles = [
        Role.CreateWithId(RoleId.New(), Customer, CustomerDescription),
        Role.CreateWithId(RoleId.New(), Contributor, ContributorDescription),
        Role.CreateWithId(RoleId.New(), Designer, DesignerDescription),
        Role.CreateWithId(RoleId.New(), Admin, AdminDescription),
    ];

    public GetAllRolesHandlerUnitTests()
    {
        cache.Setup(x => x.GetAsync<IEnumerable<Role>>(RoleKey)).ReturnsAsync(roles);
        reads.Setup(x => x.AllAsync(false, ct)).ReturnsAsync(roles);
    }

    [Fact]
    public async Task Handle_ShouldCallCache_OnCacheHit()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        cache.Verify(x => x.GetAsync<IEnumerable<Role>>(RoleKey), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldQueryDatabase_OnCacheMiss()
    {
        // Assert
        cache.Setup(x => x.GetAsync<IEnumerable<Role>>(RoleKey)).ReturnsAsync(null as Role[]);

        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads.Object, cache.Object);

        // Act
        await handler.Handle(query, ct);

        // Assert
        reads.Verify(x => x.AllAsync(false, ct), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnResult_OnCacheHit()
    {
        // Assert
        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads.Object, cache.Object);

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
        cache.Setup(x => x.GetAsync<IEnumerable<Role>>(RoleKey)).ReturnsAsync(null as Role[]);

        GetAllRolesQuery query = new();
        GetAllRolesHandler handler = new(reads.Object, cache.Object);

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
