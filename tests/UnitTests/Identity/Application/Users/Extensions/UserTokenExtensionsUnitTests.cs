using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Identity.Domain.Repositories.Writes;
using CustomCADs.Shared.Abstractions.Tokens;

namespace CustomCADs.UnitTests.Identity.Application.Users.Extensions;

public class UserTokenExtensionsUnitTests : UsersBaseUnitTests
{
	private readonly Mock<IUserWrites> writes = new();
	private readonly Mock<ITokenService> tokens = new();

	private static readonly TokenDto token = new("some-hashed-token", DateTimeOffset.UtcNow);
	private readonly User user = CreateUser();

	public UserTokenExtensionsUnitTests()
	{
		tokens.Setup(x => x.GenerateAccessToken(user.AccountId, user.Username, user.Role)).Returns(token);
		tokens.Setup(x => x.GenerateRefreshToken()).Returns(token.Value);
		tokens.Setup(x => x.GenerateCsrfToken()).Returns(token);
	}

	[Fact]
	public async Task IssueTokens_ShouldGenerateTokens()
	{
		// Arrange
		// Act
		await UserTokenExtensions.IssueTokens(writes.Object, tokens.Object, user, longerSession: true);

		// Assert
		tokens.Verify(x => x.GenerateAccessToken(user.AccountId, user.Username, user.Role), Times.Once());
		tokens.Verify(x => x.GenerateRefreshToken(), Times.Once());
		tokens.Verify(x => x.GenerateCsrfToken(), Times.Once());
	}

	[Fact]
	public async Task IssueTokens_ShouldPersistToDatabase()
	{
		// Arrange
		// Act
		await UserTokenExtensions.IssueTokens(writes.Object, tokens.Object, user, longerSession: true);

		// Assert
		writes.Verify(x => x.UpdateRefreshTokensAsync(user.Id, user.RefreshTokens.ToArray()));
	}

	[Fact]
	public async Task IssueTokens_ShouldReturnResult()
	{
		// Arrange
		// Act
		TokensDto result = await UserTokenExtensions.IssueTokens(writes.Object, tokens.Object, user, longerSession: true);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(token, result.AccessToken),
			() => Assert.Equal(token.Value, result.RefreshToken.Value), // has its own ExpiresAt generation logic
			() => Assert.Equal(token, result.CsrfToken)
		);
	}
}
