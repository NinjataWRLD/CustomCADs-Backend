﻿using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Application.Dtos;
using CustomCADs.Auth.Domain.Common.Exceptions.Users;
using CustomCADs.Auth.Domain.DomainEvents.Email;
using CustomCADs.Auth.Domain.Entities;
using CustomCADs.Shared.Application.Events;
using CustomCADs.Shared.Core.Common.TypedIds.Account;
using CustomCADs.Shared.IntegrationEvents.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CustomCADs.Auth.Infrastructure.Services;

public class AppUserService(UserManager<AppUser> manager, IEventRaiser raiser, IConfiguration config) : IUserService
{
    private readonly string serverUrl = config["URLs:Server"] ?? throw new KeyNotFoundException("Server Url not provided.");
    private readonly string clientUrl = config["URLs:Client"] ?? throw new KeyNotFoundException("Client Url not provided.");

    public async Task<AppUser?> FindByIdAsync(Guid id)
        => await manager.FindByIdAsync(id.ToString()).ConfigureAwait(false);

    public async Task<AppUser?> FindByNameAsync(string username)
        => await manager.FindByNameAsync(username).ConfigureAwait(false);

    public async Task<AppUser?> FindByEmailAsync(string email)
        => await manager.FindByEmailAsync(email).ConfigureAwait(false);

    public async Task<AppUser?> FindByRefreshTokenAsync(string rt)
        => await manager.Users.Where(u => u.RefreshToken == rt).SingleOrDefaultAsync().ConfigureAwait(false);

    public async Task<string> GetRoleAsync(AppUser user)
        => (await manager.GetRolesAsync(user).ConfigureAwait(false)).Single();

    public async Task<bool> IsLockedOutAsync(AppUser user)
        => await manager.IsLockedOutAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> CreateAsync(AppUser user)
        => await manager.CreateAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> CreateAsync(AppUser user, string password)
        => await manager.CreateAsync(user, password).ConfigureAwait(false);

    public async Task<IdentityResult> CreateUserAsync(CreateUserDto dto)
    {
        AppUser user = new(dto.Username, dto.Email);
        IdentityResult createResult = await manager.CreateAsync(user, dto.Password).ConfigureAwait(false);
        if (!createResult.Succeeded)
            return createResult;

        IdentityResult roleResult = await manager.AddToRoleAsync(user, dto.Role);
        if (!roleResult.Succeeded)
            return roleResult;

        await raiser.RaiseIntegrationEventAsync(new UserRegisteredIntegrationEvent(
            Role: dto.Role,
            Username: dto.Username,
            Email: dto.Email,
            FirstName: dto.FirstName,
            LastName: dto.LastName
        )).ConfigureAwait(false);

        return roleResult; // doesn't matter which result
    }

    public async Task<IdentityResult> UpdateAsync(AppUser user)
        => await manager.UpdateAsync(user).ConfigureAwait(false);

    public async Task<IdentityResult> UpdateAccountIdAsync(Guid id, UserId accountId)
    {
        AppUser user = await FindByIdAsync(id).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

        user.AccountId = accountId;

        var result = await manager.UpdateAsync(user).ConfigureAwait(false);
        return result;
    }

    public async Task<IdentityResult> UpdateAccountIdAsync(string username, UserId accountId)
    {
        AppUser user = await FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        user.AccountId = accountId;

        var result = await manager.UpdateAsync(user).ConfigureAwait(false);
        return result;
    }

    public async Task<IdentityResult> UpdateRefreshTokenAsync(Guid id, string rt, DateTime endDate)
    {
        AppUser user = await FindByIdAsync(id).ConfigureAwait(false)
            ?? throw UserNotFoundException.ById(id);

        user.RefreshToken = rt;
        user.RefreshTokenEndDate = endDate;

        var result = await manager.UpdateAsync(user).ConfigureAwait(false);
        return result;
    }

    public async Task<IdentityResult> RevokeRefreshTokenAsync(string username)
    {
        AppUser user = await FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        user.RefreshToken = null;
        user.RefreshTokenEndDate = null;

        var result = await manager.UpdateAsync(user).ConfigureAwait(false);
        return result;
    }

    public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role)
        => await manager.AddToRoleAsync(user, role).ConfigureAwait(false);

    public async Task<IdentityResult> RemoveFromRoleAsync(AppUser user, string oldRole)
        => await manager.RemoveFromRoleAsync(user, oldRole).ConfigureAwait(false);

    public async Task<IdentityResult> ConfirmEmailAsync(AppUser user, string token)
        => await manager.ConfirmEmailAsync(user, token).ConfigureAwait(false);

    public async Task<IdentityResult> ResetPasswordAsync(AppUser user, string token, string newPassword)
        => await manager.ResetPasswordAsync(user, token, newPassword).ConfigureAwait(false);

    public async Task<IdentityResult> DeleteAsync(AppUser user)
        => await manager.DeleteAsync(user).ConfigureAwait(false);

    public async Task SendVerificationEmailAsync(AppUser user)
    {
        string token = await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: GetVerifyEmailEndpoint(user.UserName!, token)
        )).ConfigureAwait(false);
    }

    public async Task SendVerificationEmailAsync(string username)
    {
        AppUser user = await manager.FindByNameAsync(username).ConfigureAwait(false)
            ?? throw UserNotFoundException.ByUsername(username);

        string token = await manager.GenerateEmailConfirmationTokenAsync(user).ConfigureAwait(false);

        await raiser.RaiseDomainEventAsync(new EmailVerificationRequestedDomainEvent(
            Email: user.Email ?? string.Empty,
            Endpoint: GetVerifyEmailEndpoint(user.UserName!, token)
        )).ConfigureAwait(false);
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

    private string GetVerifyEmailEndpoint(string username, string token)
        => $"{serverUrl}/api/v1/auth/signup/verifyEmail/{username}?token={token}";

    private string GetResetPasswordPage(string email, string token)
        => $"{clientUrl}/login/reset-password?email={email}&token={token}";
}