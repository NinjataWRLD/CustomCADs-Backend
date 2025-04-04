using CustomCADs.Identity.Application.Common.Dtos;

namespace CustomCADs.Identity.Application.Common.Contracts;

public interface IUserService
{
    Task RegisterAsync(CreateUserDto dto, string timeZone, string? firstName, string? lastName);
    Task SendVerificationEmailAsync(string username, Func<string, string> getUri);
    Task<TokensDto> ConfirmEmailAsync(string username, string token);
    Task<TokensDto> LoginAsync(LoginCommand req);
    Task SendResetPasswordEmailAsync(string email);
    Task ResetPasswordAsync(string email, string token, string newPassword);
    Task<AccessTokenDto> RefreshAsync(string? rt);
    Task LogoutAsync(string username);
}
