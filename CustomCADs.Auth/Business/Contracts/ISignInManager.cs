using CustomCADs.Auth.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace CustomCADs.Auth.Business.Contracts;

public interface ISignInManager
{
    Task<SignInResult> PasswordSignInAsync(AppUser user, string password, bool isPersistent, bool lockoutOnFailure);
    Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);
}
