using CustomCADs.Identity.Domain.Users.Entities;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.RemoveRefreshToken;

public class UserRemoveRefreshTokenUnitTests : UsersBaseUnitTests
{
	private static readonly User user = CreateUser();
	private readonly RefreshToken rt;

	public UserRemoveRefreshTokenUnitTests()
	{
		rt = user.AddRefreshToken("refresh-token", longerSession: false);
	}

	[Fact]
	public void RemoveRefreshToken_ShouldNotThrowException()
	{
		user.RemoveRefreshToken(rt);
	}

	[Fact]
	public void RemoveRefreshToken_ShouldReturnResult()
	{
		bool firstResult = user.RemoveRefreshToken(rt);
		bool secondResult = user.RemoveRefreshToken(rt);

		Assert.Multiple(
			() => Assert.True(firstResult),
			() => Assert.False(secondResult)
		);
	}

	[Fact]
	public void RemoveRefreshToken_PopulatesProperty()
	{
		user.RemoveRefreshToken(rt);
		Assert.DoesNotContain(rt, user.RefreshTokens);
	}
}
