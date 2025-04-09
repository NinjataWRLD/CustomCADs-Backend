using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Persistence.Managers;

internal static class Mapper
{
    internal static User ToUser(this AppUser appUser, string role)
        => User.Create(
            id: UserId.New(appUser.Id),
            role: role,
            username: appUser.UserName ?? string.Empty,
            email: new(appUser.Email ?? string.Empty, appUser.EmailConfirmed),
            refreshToken: appUser.RefrehToken,
            accountId: appUser.AccountId
        );

    internal static AppUser ToAppUser(this User user)
        => new()
        {
            Id = user.Id.Value,
            UserName = user.Username,
            Email = user.Email.Value,
            EmailConfirmed = user.Email.IsVerified,
            RefrehToken = user.RefreshToken,
            AccountId = user.AccountId,
        };
}
