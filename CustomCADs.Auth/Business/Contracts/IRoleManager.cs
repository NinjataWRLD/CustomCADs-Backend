using CustomCADs.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Business.Contracts;

public interface IRoleManager
{
    Task<IEnumerable<AppRole>> GetRoles();
    Task<IEnumerable<string>> GetRolesNames();
    Task<AppRole?> FindByNameAsync(string name);
    Task<bool> RoleExistsAsync(string role);
    Task<IdentityResult> CreateAsync(AppRole role);
    Task UpdateAsync(AppRole role);
    Task DeleteAsync(AppRole role);
}
