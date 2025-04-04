using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain;
using CustomCADs.Identity.Domain.DomainEvents.Email;
using CustomCADs.Identity.Domain.Repositories;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Application.Common.Services;

using static AccountConstants;

public sealed class AppUserService(IUserRepository repository, ITokenService tokenService, IEventRaiser raiser, IRequestSender sender, IOptions<ClientUrlSettings> settings) : IUserService
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

        bool success = await repository.AddAsync(
            role: dto.Role,
            user: new(dto.Username, dto.Email, accountId),
            password: dto.Password
        ).ConfigureAwait(false);

        if (!success)
            throw UserCreationException.ByUsername(dto.Username);
    }

    public async Task SendVerificationEmailAsync(string username, Func<string, string> getUri)
    {
        AppUser user = await repository.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        string token = await repository.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: getUri(token)
        )).ConfigureAwait(false);
    }

    public async Task<TokensDto> ConfirmEmailAsync(string username, string token)
    {
        AppUser? user = await repository.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        if (user.EmailConfirmed)
            throw UserRegisterException.AlreadyConfirmed(user.UserName ?? string.Empty);

        bool success = await repository.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        if (!success)
            throw UserRegisterException.EmailToken(user.UserName ?? string.Empty);

        return await IssueTokens(username, false).ConfigureAwait(false);
    }

    public async Task<TokensDto> LoginAsync(LoginCommand req)
    {
        AppUser user = await repository.GetByUsernameAsync(req.Username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(req.Username);

        EnsureEmailConfirmed(user);
        await EnsureNotLockedOutAsync(user).ConfigureAwait(false);
        await EnsureValidPasswordAsync(user, req.Password).ConfigureAwait(false);

        return await IssueTokens(req.Username, req.LongerExpireTime).ConfigureAwait(false);
    }

    public async Task SendResetPasswordEmailAsync(string email)
    {
        AppUser user = await repository.GetByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

        string token = await repository.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new PasswordResetRequestedDomainEvent(
            Email: email,
            Endpoint: GetResetPasswordPage(email, token)
        )).ConfigureAwait(false);
    }

    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        AppUser? user = await repository.GetByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

        bool succeess = await repository.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
        if (!succeess)
            throw UserPasswordException.ResetFailure(user.UserName ?? string.Empty);
    }

    public async Task<AccessTokenDto> RefreshAsync(string? rt)
    {
        if (string.IsNullOrEmpty(rt))
        {
            throw UserRefreshTokenException.Missing();
        }

        AppUser user = await repository.GetByRefreshTokenAsync(rt).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByRefreshToken(rt);

        if (user.RefreshTokenEndDate < DateTime.UtcNow)
        {
            throw UserRefreshTokenException.Expired();
        }

        return tokenService.GenerateAccessToken(
            user.AccountId,
            user.UserName ?? string.Empty,
            role: await repository.GetRoleAsync(user).ConfigureAwait(false)
        );
    }

    public async Task LogoutAsync(string username)
    {
        AppUser user = await repository.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        user.RefreshToken = null;
        user.RefreshTokenEndDate = null;

        await repository.SaveChangesAsync().ConfigureAwait(false);
    }

    private async Task<TokensDto> IssueTokens(string username, bool rememberMe)
    {
        AppUser user = await repository.GetByUsernameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        string role = await repository.GetRoleAsync(user).ConfigureAwait(false);
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
        AppUser user = await repository.GetByIdAsync(id).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

        int days = longerSession ? LongerRtDurationInDays : RtDurationInDays;
        DateTime rtEndDate = DateTime.UtcNow.AddDays(days);
        string rtValue = tokenService.GenerateRefreshToken();

        user.RefreshToken = rtValue;
        user.RefreshTokenEndDate = rtEndDate;

        await repository.SaveChangesAsync().ConfigureAwait(false);
        return new(rtValue, rtEndDate);
    }

    private static void EnsureEmailConfirmed(AppUser user)
    {
        if (!user.EmailConfirmed)
        {
            throw UserLoginException.NotConfirmed(user.UserName ?? string.Empty);
        }
    }

    private async Task EnsureNotLockedOutAsync(AppUser user)
    {
        bool isLockedOut = await repository.GetIsLockedOutAsync(user).ConfigureAwait(false);
        if (isLockedOut && user.LockoutEnd.HasValue)
        {
            TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
            throw UserLockedOutException.ByUsername(user.UserName ?? string.Empty, seconds);
        }
    }

    private async Task EnsureValidPasswordAsync(AppUser user, string password)
    {
        if (!await repository.CheckPasswordAsync(user, password).ConfigureAwait(false))
        {
            throw UserLoginException.ByUsername(user.UserName ?? string.Empty);
        }
    }

    private string GetResetPasswordPage(string email, string token)
        => $"{clientUrl}/reset-password?email={email}&token={token}";
}
