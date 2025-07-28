using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;

namespace CustomCADs.Identity.Domain.Repositories.Reads;

public interface IUserReads
{
	Task<User?> GetByUsernameAsync(string username);
	Task<User?> GetByEmailAsync(string email);
	Task<(User? User, RefreshToken? RefreshToken)> GetByRefreshTokenAsync(string token);
	Task<DateTimeOffset?> GetIsLockedOutAsync(string username);
}
