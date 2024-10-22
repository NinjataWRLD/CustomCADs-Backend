namespace CustomCADs.Account.Domain.Roles.Reads;

public interface IRoleReads
{
    Task<IEnumerable<Role>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<Role?> SingleByIdAsync(int id, bool track = true, CancellationToken ct = default);
    Task<Role?> SingleByNameAsync(string name, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsByNameAsync(string name, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
