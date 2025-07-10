using CustomCADs.Identity.Domain.Users;
using CustomCADs.Identity.Domain.Users.Entities;
using CustomCADs.Shared.Core;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.AddRefreshToken;

using static UsersData;
using static Constants;

public class UserAddRefreshTokenUnitTests : UsersBaseUnitTests
{
	private const string Value = "refresh-token";
	private static readonly User user = CreateUser();

	[Fact]
	public void AddRefreshToken_ShouldNotThrowException()
	{
		user.AddRefreshToken(Value, longerSession: false);
	}

	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public void AddRefreshToken_ShouldReturnResult(bool longerSession)
	{
		int expectedDurationDays = longerSession
			? Tokens.LongerRtDurationInDays
			: Tokens.RtDurationInDays;
		TimeSpan expectedDuration = TimeSpan.FromDays(expectedDurationDays);

		RefreshToken rt = user.AddRefreshToken(Value, longerSession);
		TimeSpan actualDuration = rt.ExpiresAt - rt.IssuedAt;

		Assert.Multiple(
			() => Assert.Equal(Value, rt.Value),
			() => Assert.Equal(user.Id, rt.UserId),
			() => Assert.Equal(expectedDuration, actualDuration)
		);
	}

	[Fact]
	public void AddRefreshToken_PopulatesProperty()
	{
		RefreshToken rt = user.AddRefreshToken(Value, longerSession: false);
		Assert.Contains(rt, user.RefreshTokens);
	}
}
