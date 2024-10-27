using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Presentation;

public class AppSignInService(SignInManager<AppUser> manager) : ISignInService
{
    public async Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);

    public async Task<SignInResult> PasswordSignInAsync(string username, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);
}