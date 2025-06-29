﻿using CustomCADs.Identity.Domain.Users;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Domain.Managers;

public interface IUserManager
{
	Task<User?> GetByIdAsync(UserId id);
	Task<User?> GetByUsernameAsync(string username);
	Task<User?> GetByEmailAsync(string email);
	Task<User?> GetByRefreshTokenAsync(string token);
	Task<DateTimeOffset?> GetIsLockedOutAsync(string username);
	Task<bool> AddAsync(User user, string password);
	Task<string> GenerateEmailConfirmationTokenAsync(User user);
	Task<bool> ConfirmEmailAsync(User user, string token);
	Task<string> GeneratePasswordResetTokenAsync(User user);
	Task<bool> ResetPasswordAsync(User user, string token, string newPassword);
	Task<bool> CheckPasswordAsync(User user, string password);
	Task UpdateAsync(UserId id, User user);
	Task DeleteAsync(string username);
}
