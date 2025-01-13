using CustomCADs.Identity.Application.Common.Dtos;

namespace CustomCADs.Identity.Application.Common.Contracts;

public interface IUserService
{
    Task<AppUser> FindByIdAsync(Guid id);
    Task<AppUser> FindByNameAsync(string username);
    Task<AppUser> FindByEmailAsync(string email);
    Task<AppUser> FindByRefreshTokenAsync(string rt);
    Task<string> GetRoleAsync(AppUser user);
    Task CreateAsync(CreateUserDto dto);
    Task CreateAsync(AppUser user, string password);
    Task<RefreshTokenDto> UpdateRefreshTokenAsync(Guid id);
    Task<IdentityResult> RevokeRefreshTokenAsync(string username);
    Task<IdentityResult> AddToRoleAsync(AppUser user, string role);
    Task ConfirmEmailAsync(AppUser user, string token);
    Task ResetPasswordAsync(AppUser user, string token, string newPassword);
    Task<IdentityResult> DeleteAsync(AppUser user);
    Task<LoginDto> LoginAsync(LoginCommand req);
    Task<RefreshDto> RefreshAsync(string? rt);
    Task SendVerificationEmailAsync(string username, string uri);
    Task SendVerificationEmailAsync(AppUser user, string uri);
    Task<string> GenerateEmailConfirmationTokenAsync(string username);
    Task<string> GenerateEmailConfirmationTokenAsync(AppUser user);
    Task SendResetPasswordEmailAsync(AppUser user);
    Task SendResetPasswordEmailAsync(string email);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
}
