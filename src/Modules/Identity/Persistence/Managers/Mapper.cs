using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Persistence.ShadowEntities;

namespace CustomCADs.Identity.Persistence.Managers;

internal static class Mapper
{
    internal static User ToUser(this AppUser appUser)
        => new()
        {
            Id = appUser.Id,
            Username = appUser.UserName ?? string.Empty,
            Email = new(appUser.Email ?? string.Empty, appUser.EmailConfirmed),
            RefreshToken = appUser.RefrehToken,
            AccountId = appUser.AccountId,
        };

    internal static AppUser ToAppUser(this User user)
        => new()
        {
            Id = user.Id,
            UserName = user.Username,
            Email = user.Email.Value,
            EmailConfirmed = user.Email.IsVerified,
            RefrehToken = user.RefreshToken,
            AccountId = user.AccountId,
        };
}
