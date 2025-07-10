using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Repositories.Writes;

public interface IUserWrites
{
	Task<bool> CreateAsync(User user, string password);
	Task<string> GenerateEmailConfirmationTokenAsync(User user);
	Task<bool> ConfirmEmailAsync(User user, string token);
	Task<string> GeneratePasswordResetTokenAsync(User user);
	Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
	Task<bool> CheckPasswordAsync(User user, string password);
	Task UpdateUsernameAsync(UserId id, string username);
	Task UpdateRefreshTokensAsync(UserId id, RefreshToken[] refreshTokens);
	Task DeleteAsync(string username);
}
