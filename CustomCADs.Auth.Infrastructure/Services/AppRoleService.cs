using CustomCADs.Auth.Application.Common.Contracts;
using CustomCADs.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Auth.Infrastructure.Services;

public sealed class AppRoleService(RoleManager<AppRole> manager) : IRoleService
{
    public async Task<IEnumerable<AppRole>> GetRoles()
        => await manager.Roles.ToArrayAsync().ConfigureAwait(false);

    public async Task<IEnumerable<string>> GetRolesNames()
        => await manager.Roles.Select(r => r.Name ?? "").ToArrayAsync().ConfigureAwait(false);

    public async Task<AppRole?> FindByNameAsync(string name)
        => await manager.FindByNameAsync(name).ConfigureAwait(false);

    public async Task<bool> RoleExistsAsync(string role)
        => await manager.RoleExistsAsync(role).ConfigureAwait(false);

    public async Task<IdentityResult> CreateAsync(AppRole role)
        => await manager.CreateAsync(role).ConfigureAwait(false);

    public async Task UpdateAsync(AppRole role)
        => await manager.UpdateAsync(role).ConfigureAwait(false);
    public async Task DeleteAsync(AppRole role)
        => await manager.DeleteAsync(role).ConfigureAwait(false);
}
