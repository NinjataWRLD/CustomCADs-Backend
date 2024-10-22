namespace CustomCADs.Account.Domain.Users.Reads;

public interface IUserReads
{
    Task<IEnumerable<User>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<User?> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<User?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
