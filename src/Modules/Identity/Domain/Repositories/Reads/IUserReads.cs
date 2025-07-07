using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Repositories.Reads;

public interface IUserReads
{
	Task<User?> GetByIdAsync(UserId id);
	Task<User?> GetByUsernameAsync(string username);
	Task<User?> GetByEmailAsync(string email);
	Task<(User? User, RefreshToken? RefreshToken)> GetByRefreshTokenAsync(string token);
	Task<DateTimeOffset?> GetIsLockedOutAsync(string username);
}
