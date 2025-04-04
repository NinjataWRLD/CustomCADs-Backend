using CustomCADs.Identity.Domain.Entities;
using CustomCADs.Identity.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CustomCADs.Identity.Infrastructure.Repositories;

public class AppRoleRepoistory(IdentityContext context) : IRoleRepository
{
    public async Task<AppRole?> GetByNameAsync(string name)
        => await context.Roles.FirstOrDefaultAsync(x => x.Name == name).ConfigureAwait(false);

    public async Task<AppRole> AddAsync(AppRole role)
        => (await context.Roles.AddAsync(role).ConfigureAwait(false)).Entity;

    public void Remove(AppRole role)
        => context.Roles.Remove(role);

    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync().ConfigureAwait(false);
}
