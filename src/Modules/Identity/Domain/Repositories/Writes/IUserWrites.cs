using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Repositories.Writes;

public interface IUserWrites
{
	Task<bool> CreateAsync(User user, string password);
	Task<string> GenerateEmailConfirmationTokenAsync(string username);
	Task<bool> ConfirmEmailAsync(string username, string token);
	Task<string> GeneratePasswordResetTokenAsync(string username);
	Task<bool> ResetPasswordAsync(string username, string token, string newPassword);
	Task<bool> CheckPasswordAsync(string username, string password);
	Task UpdateUsernameAsync(UserId id, string username);
	Task UpdateRefreshTokensAsync(UserId id, RefreshToken[] refreshTokens);
	Task DeleteAsync(string username);
}
