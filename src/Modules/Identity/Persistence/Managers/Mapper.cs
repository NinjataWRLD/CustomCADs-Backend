using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Identity.Persistence.ShadowEntities;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.Identity.Persistence.Managers;

internal static class Mapper
{
	private static class Shallow
	{
		internal static RefreshToken ToRefreshToken(AppRefreshToken rt)
			=> RefreshToken.Create(
				id: RefreshTokenId.New(rt.Id),
				value: rt.Value,
				userId: UserId.New(rt.UserId),
				issuedAt: rt.IssuedAt,
				expiresAt: rt.ExpiresAt
			);

		internal static AppRefreshToken ToAppRefreshToken(RefreshToken rt)
			=> new(rt.Value, rt.UserId.Value, rt.IssuedAt, rt.ExpiresAt)
			{
				Id = rt.Id.Value,
			};
	}

	internal static User ToUser(this AppUser appUser, string role)
		=> User.Create(
			id: UserId.New(appUser.Id),
			role: role,
			username: appUser.UserName ?? string.Empty,
			email: new(appUser.Email ?? string.Empty, appUser.EmailConfirmed),
			accountId: appUser.AccountId,
			refreshTokens: [.. appUser.RefreshTokens.Select(x => Shallow.ToRefreshToken(x))]
		);

	internal static AppUser ToAppUser(this User user)
		=> new AppUser()
		{
			Id = user.Id.Value,
			UserName = user.Username,
			Email = user.Email.Value,
			EmailConfirmed = user.Email.IsVerified,
			AccountId = user.AccountId,
		}.FillRefreshTokens([.. user.RefreshTokens.Select(x => Shallow.ToAppRefreshToken(x))]);

	internal static RefreshToken ToRefreshToken(this AppRefreshToken rt, string role)
		=> RefreshToken.Create(
			id: RefreshTokenId.New(rt.Id),
			value: rt.Value,
			userId: UserId.New(rt.UserId),
			issuedAt: rt.IssuedAt,
			expiresAt: rt.ExpiresAt
		);

	internal static AppRefreshToken ToAppRefreshToken(this RefreshToken rt)
		=> new(rt.Value, rt.UserId.Value, rt.IssuedAt, rt.ExpiresAt)
		{
			Id = rt.Id.Value,
		};
}
