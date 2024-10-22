namespace CustomCADs.Account.Domain.Roles.Reads;

public interface IRoleReads
{
    Task<IEnumerable<Role>> AllAsync();
    Task<Role?> SingleByIdAsync(int id);
    Task<Role?> SingleByNameAsync(string name);
    Task<bool> ExistsByIdAsync(int id);
    Task<bool> ExistsByNameAsync(string name);
    Task<int> CountAsync()
}
