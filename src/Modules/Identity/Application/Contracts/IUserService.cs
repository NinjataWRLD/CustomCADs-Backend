using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.Identity.Application.Contracts;

public interface IUserService
{
	#region GetUserByX
	Task<User> GetByUsernameAsync(string username);
	Task<(User User, RefreshToken RefreshToken)> GetByRefreshTokenAsync(string token);
	#endregion

	#region GetPropertyX
	Task<AccountId> GetAccountIdAsync(string username);
	Task<DateTimeOffset?> GetIsLockedOutAsync(string username);
	#endregion

	#region Lifecycle
	Task CreateAsync(User user, string password);
	Task DeleteAsync(string username);
	#endregion

	#region Mutation
	Task<bool> CheckPasswordAsync(string username, string password);
	Task UpdateUsernameAsync(UserId id, string username);
	Task<RefreshToken> AddRefreshTokenAsync(User user, string token, bool longerSession);
	Task RevokeRefreshTokenAsync(string token);
	#endregion

	#region Token Generation
	Task<string> GenerateEmailConfirmationTokenAsync(string username);
	Task ConfirmEmailAsync(string username, string token);
	Task<string> GeneratePasswordResetTokenAsync(string email);
	Task ResetPasswordAsync(string email, string token, string newPassword);
	#endregion
}
