using CustomCADs.Auth.Business.Contracts;
using CustomCADs.Auth.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Business.Managers;

public class AppSignInManager(SignInManager<AppUser> manager) : ISignInManager
{
    public async Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);

    public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        => await manager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure).ConfigureAwait(false);
}
