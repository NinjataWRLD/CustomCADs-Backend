using CustomCADs.Identity.Application.Common.Contracts;
using CustomCADs.Identity.Application.Common.Dtos;
using CustomCADs.Identity.Application.Common.Exceptions;
using CustomCADs.Identity.Domain;
using CustomCADs.Identity.Domain.DomainEvents.Email;
using CustomCADs.Identity.Domain.Entities;
using CustomCADs.Identity.Infrastructure.Dtos;
using CustomCADs.Shared.Abstractions.Events;
using CustomCADs.Shared.Abstractions.Requests.Sender;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.UseCases.Accounts.Commands;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CustomCADs.Identity.Infrastructure.Services;

using static AccountConstants;

public sealed class AppUserService(UserManager<AppUser> manager, ITokenService tokenService, IEventRaiser raiser, IRequestSender sender, IOptions<ClientUrlSettings> settings) : IUserService
{
    private readonly string clientUrl = settings.Value.Preferred;

    public async Task<AppUser> FindByIdAsync(Guid id)
        => await manager.FindByIdAsync(id.ToString()).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

    public async Task<AppUser> FindByNameAsync(string username)
        => await manager.FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

    public async Task<AppUser> FindByEmailAsync(string email)
        => await manager.FindByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

    public async Task<AppUser> FindByRefreshTokenAsync(string rt)
        => await manager.Users.Where(u => u.RefreshToken == rt).SingleOrDefaultAsync().ConfigureAwait(false)
            ?? throw UserNotFoundException.ByRefreshToken(rt);

    public async Task<string> GetRoleAsync(AppUser user)
        => (await manager.GetRolesAsync(user).ConfigureAwait(false)).Single();

    public async Task CreateAsync(AppUser user, string password)
    {
        var result = await manager.CreateAsync(user, password).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw UserCreationException.ByUsername(user.UserName ?? string.Empty);
        }
    }

    public async Task CreateAsync(CreateUserDto dto)
    {
        CreateAccountCommand command = new(
            Role: dto.Role,
            Username: dto.Username,
            Email: dto.Email,
            TimeZone: dto.TimeZone,
            FirstName: dto.FirstName,
            LastName: dto.LastName
        );
        AccountId accountId = await sender.SendCommandAsync(command).ConfigureAwait(false);

        AppUser user = new(dto.Username, dto.Email, accountId);
        IdentityResult createResult = await manager.CreateAsync(user, dto.Password).ConfigureAwait(false);
        if (!createResult.Succeeded)
        {
            throw UserCreationException.ByUsername(dto.Username);
        }

        IdentityResult roleResult = await manager.AddToRoleAsync(user, dto.Role).ConfigureAwait(false);
        if (!roleResult.Succeeded)
        {
            throw UserCreationException.WithRole(dto.Username, dto.Role);
        }
    }

    public async Task<RefreshTokenDto> UpdateRefreshTokenAsync(Guid id, bool longerSession)
    {
        AppUser user = await FindByIdAsync(id).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

        int days = longerSession ? LongerRtDurationInDays : RtDurationInDays;
        DateTime rtEndDate = DateTime.UtcNow.AddDays(days);
        string rtValue = tokenService.GenerateRefreshToken();

        user.RefreshToken = rtValue;
        user.RefreshTokenEndDate = rtEndDate;

        await manager.UpdateAsync(user).ConfigureAwait(false);
        return new(rtValue, rtEndDate);
    }

    public async Task<IdentityResult> RevokeRefreshTokenAsync(string username)
    {
        AppUser user = await FindByNameAsync(username).ConfigureAwait(false);

        user.RefreshToken = null;
        user.RefreshTokenEndDate = null;

        var result = await manager.UpdateAsync(user).ConfigureAwait(false);
        return result;
    }

    public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        => await manager.AddToRoleAsync(user, role).ConfigureAwait(false);

    public async Task ConfirmEmailAsync(AppUser user, string token)
    {
        if (user.EmailConfirmed)
        {
            throw UserRegisterException.AlreadyConfirmed(user.UserName ?? string.Empty);
        }

        var result = await manager.ConfirmEmailAsync(user, token).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw UserRegisterException.EmailToken(user.UserName ?? string.Empty);
        }
    }

    public async Task ResetPasswordAsync(AppUser user, string token, string newPassword)
    {
        var result = await manager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);
        if (!result.Succeeded)
        {
            throw UserPasswordException.ResetFailure(user.UserName ?? string.Empty);
        }
    }

    public async Task<IdentityResult> DeleteAsync(AppUser user)
        => await manager.DeleteAsync(user).ConfigureAwait(false);

    public async Task SendVerificationEmailAsync(AppUser user, string uri)
    {
        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: uri
        )).ConfigureAwait(false);
    }

    public async Task SendVerificationEmailAsync(string username, string uri)
    {
        AppUser user = await manager.FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: uri
        )).ConfigureAwait(false);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(AppUser user)
    {
        if (user.EmailConfirmed)
        {
            throw UserRegisterException.AlreadyConfirmed(user.UserName ?? string.Empty);
        }

        return await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(string username)
    {
        AppUser user = await manager.FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        return await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);
    }

    public async Task SendResetPasswordEmailAsync(AppUser user)
    {
        string token = await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new PasswordResetRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: GetResetPasswordPage(user.Email!, token)
        )).ConfigureAwait(false);
    }

    public async Task SendResetPasswordEmailAsync(string email)
    {
        AppUser user = await manager.FindByEmailAsync(email).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByEmail(email);

        string token = await manager.GeneratePasswordResetTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new PasswordResetRequestedDomainEvent(
            Email: email,
            Endpoint: GetResetPasswordPage(email, token)
        )).ConfigureAwait(false);
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        bool success = await manager.CheckPasswordAsync(user, password).ConfigureAwait(false);

        if (success)
            await manager.ResetAccessFailedCountAsync(user).ConfigureAwait(false);
        else
            await manager.AccessFailedAsync(user).ConfigureAwait(false);

        return success;
    }

    public static void EnsureEmailConfirmed(AppUser user)
    {
        if (!user.EmailConfirmed)
        {
            throw UserLoginException.NotConfirmed(user.UserName ?? string.Empty);
        }
    }

    public async Task EnsureNotLockedOutAsync(AppUser user)
    {
        bool isLockedOut = await manager.IsLockedOutAsync(user).ConfigureAwait(false);
        if (isLockedOut && user.LockoutEnd.HasValue)
        {
            TimeSpan timeLeft = user.LockoutEnd.Value.Subtract(DateTimeOffset.UtcNow);
            int seconds = Convert.ToInt32(timeLeft.TotalSeconds);
            throw UserLockedOutException.ByUsername(user.UserName ?? string.Empty, seconds);
        }
    }

    public async Task EnsureValidPasswordAsync(AppUser user, string password)
    {
        bool isPasswordValid = await CheckPasswordAsync(user, password).ConfigureAwait(false);
        if (!isPasswordValid)
        {
            throw UserLoginException.ByUsername(user.UserName ?? string.Empty);
        }
    }

    public async Task<LoginDto> LoginAsync(LoginCommand req)
    {
        AppUser user = await FindByNameAsync(req.Username).ConfigureAwait(false);

        EnsureEmailConfirmed(user);
        await EnsureNotLockedOutAsync(user).ConfigureAwait(false);
        await EnsureValidPasswordAsync(user, req.Password).ConfigureAwait(false);

        string role = await GetRoleAsync(user).ConfigureAwait(false);
        AccessTokenDto jwt = tokenService.GenerateAccessToken(user.AccountId, req.Username, role);

        RefreshTokenDto rt = await UpdateRefreshTokenAsync(user.Id, req.LongerExpireTime).ConfigureAwait(false);

        return new(
            Role: role,
            AccessToken: jwt,
            RefreshToken: rt
        );
    }

    public async Task<RefreshDto> RefreshAsync(string? rt)
    {
        if (string.IsNullOrEmpty(rt))
        {
            throw UserRefreshTokenException.Missing();
        }

        AppUser user = await FindByRefreshTokenAsync(rt).ConfigureAwait(false);
        if (user.RefreshTokenEndDate < DateTime.UtcNow)
        {
            throw UserRefreshTokenException.Expired();
        }

        string username = user.UserName ?? string.Empty,
            role = await GetRoleAsync(user).ConfigureAwait(false);

        AccessTokenDto newJwt = tokenService.GenerateAccessToken(user.AccountId, username, role);
        RefreshTokenDto? newRt = null;

        if (user.RefreshTokenEndDate < DateTime.UtcNow.AddMinutes(1))
        {
            newRt = await UpdateRefreshTokenAsync(user.Id, longerSession: false).ConfigureAwait(false);
        }

        return new(
            Username: username,
            Role: role,
            AccessToken: newJwt,
            RefreshToken: newRt
        );
    }

    private string GetResetPasswordPage(string email, string token)
        => $"{clientUrl}/reset-password?email={email}&token={token}";
}
