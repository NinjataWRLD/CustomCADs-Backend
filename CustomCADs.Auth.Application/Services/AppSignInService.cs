using CustomCADs.Auth.Application.Contracts;
using CustomCADs.Auth.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Application.Services;

public class AppSignInService(SignInManager<AppUser> manager) : ISignInService
{
    public async Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);
}
