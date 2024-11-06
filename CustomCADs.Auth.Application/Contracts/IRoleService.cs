namespace CustomCADs.Auth.Application.Contracts;

public interface IRoleService
{
    Task<IEnumerable<AppRole>> GetRoles();
    Task<IEnumerable<string>> GetRolesNames();
    Task<AppRole?> FindByNameAsync(string name);
    Task<bool> RoleExistsAsync(string role);
    Task<IdentityResult> CreateAsync(AppRole role);
    Task UpdateAsync(AppRole role);
    Task DeleteAsync(AppRole role);
}
