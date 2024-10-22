namespace CustomCADs.Account.Domain.Users.Reads;

public interface IUserReads
{
    Task<IEnumerable<User>> AllAsync();
    Task<User?> SingleByIdAsync(Guid id);
    Task<User?> SingleByUsernameAsync(string username);
    Task<bool> ExistsByIdAsync(Guid id);
    Task<bool> ExistsByUsernameAsync(string username);
    Task<int> CountAsync();
}
