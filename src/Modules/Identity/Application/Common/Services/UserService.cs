using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain;
using CustomCADs.Identity.Domain.DomainEvents.Email;
using CustomCADs.Identity.Domain.Managers;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Common.Services;

using static IdentityConstants;

public sealed class UserService(IUserManager manager, ITokenService tokenService, IEventRaiser raiser, IRequestSender sender, IOptions<ClientUrlSettings> settings) : IUserService
{
    private readonly string clientUrl = settings.Value.Preferred;

    public async Task RegisterAsync(CreateUserDto dto, string timeZone, string? firstName, string? lastName)
    {
        CreateAccountCommand command = new(
            Role: dto.Role,
            Username: dto.Username,
            Email: dto.Email,
            TimeZone: timeZone,
            FirstName: firstName,
            LastName: lastName
        );
        AccountId accountId = await sender.SendCommandAsync(command).ConfigureAwait(false);

        bool success = await manager.AddAsync(
            user: new(
                role: dto.Role,
                username: dto.Username,
                email: new(dto.Email, IsVerified: false),
                accountId: accountId
            ),
            password: dto.Password
        ).ConfigureAwait(false);

        if (!success)
            throw UserCreationException.ByUsername(dto.Username);
    }

    public async Task SendVerificationEmailAsync(string username, Func<string, string> getUri)
    {
        User user = await manager.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        string token = await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email.Value,
            Endpoint: getUri(token)
        )).ConfigureAwait(false);
    }

    public async Task<TokensDto> ConfirmEmailAsync(string username, string token)
    {
        User? user = await manager.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        if (user.Email.IsVerified)
            throw UserRegisterException.AlreadyConfirmed(user.Username);

        bool success = await manager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        if (!success)
            throw UserRegisterException.EmailToken(user.Username);

        return await IssueTokens(username, false).ConfigureAwait(false);
    }

    public async Task<TokensDto> LoginAsync(LoginCommand req)
    {
        User user = await manager.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(req.Username);

        EnsureEmailConfirmed(user);
        await EnsureNotLockedOutAsync(user).ConfigureAwait(false);
        await EnsureValidPasswordAsync(user, req.Password).ConfigureAwait(false);

        return await IssueTokens(req.Username, req.LongerExpireTime).ConfigureAwait(false);
    }

    public async Task SendResetPasswordEmailAsync(string email)
    {
        User user = await manager.GetByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

        string token = await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new PasswordResetRequestedDomainEvent(
            Email: email,
            Endpoint: GetResetPasswordPage(email, token)
        )).ConfigureAwait(false);
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        User? user = await manager.GetByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

        bool succeess = await manager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
        if (!succeess)
            throw UserPasswordException.ResetFailure(user.Username);
    }

    public async Task<AccessTokenDto> RefreshAsync(string? rt)
    {
        if (string.IsNullOrEmpty(rt))
        {
            throw UserRefreshTokenException.Missing();
        }

        User user = await manager.GetByRefreshTokenAsync(rt).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByRefreshToken(rt);

        if (user.RefreshToken?.ExpiresAt < DateTime.UtcNow)
        {
            throw UserRefreshTokenException.Expired();
        }

        return tokenService.GenerateAccessToken(
            user.AccountId,
            user.Username,
            role: await manager.GetRoleAsync(user.Username).ConfigureAwait(false)
        );
    }

    public async Task LogoutAsync(string username)
    {
        User user = await manager.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        user.RefreshToken = null;
        await manager.UpdateAsync(user).ConfigureAwait(false);
    }

    private async Task<TokensDto> IssueTokens(string username, bool rememberMe)
    {
        User user = await manager.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        string role = await manager.GetRoleAsync(username).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.AccountId, username, role);
        RefreshTokenDto rt = await UpdateRefreshTokenAsync(user.Id, longerSession: rememberMe).ConfigureAwait(false);

        return new(
            Role: role,
            AccessToken: jwt,
            RefreshToken: rt
        );

    }

    private async Task<RefreshTokenDto> UpdateRefreshTokenAsync(Guid id, bool longerSession)
    {
        User user = await manager.GetByIdAsync(id).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

        int days = longerSession ? LongerRtDurationInDays : RtDurationInDays;
        DateTime rtEndDate = DateTime.UtcNow.AddDays(days);
        string rtValue = tokenService.GenerateRefreshToken();

        user.RefreshToken = new(rtValue, rtEndDate);
        await manager.UpdateAsync(user).ConfigureAwait(false);

        return new(rtValue, rtEndDate);
    }

    private static void EnsureEmailConfirmed(User user)
    {
        if (!user.Email.IsVerified)
        {
            throw UserLoginException.NotConfirmed(user.Username);
        }
    }

    private async Task EnsureNotLockedOutAsync(User user)
    {
        DateTimeOffset? lockoutEnd = await manager.GetIsLockedOutAsync(user.Username).ConfigureAwait(false);
        if (lockoutEnd.HasValue)
        {
            TimeSpan timeLeft = lockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
            throw UserLockedOutException.ByUsername(user.Username, seconds);
        }
    }

    private async Task EnsureValidPasswordAsync(User user, string password)
    {
        if (!await manager.CheckPasswordAsync(user, password).ConfigureAwait(false))
        {
            throw UserLoginException.ByUsername(user.Username);
        }
    }

    private string GetResetPasswordPage(string email, string token)
        => $"{clientUrl}/reset-password?email={email}&token={token}";
}
