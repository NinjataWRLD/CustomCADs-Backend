using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Account.Domain.Users.Reads;

public interface IUserReads
{
    Task<Result<User>> AllAsync(UserQuery query, bool track = true, CancellationToken ct = default);
    Task<User?> SingleByIdAsync(UserId id, bool track = true, CancellationToken ct = default);
    Task<User?> SingleByUsernameAsync(string username, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(UserId id, CancellationToken ct = default);
    Task<bool> ExistsByUsernameAsync(string username, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
