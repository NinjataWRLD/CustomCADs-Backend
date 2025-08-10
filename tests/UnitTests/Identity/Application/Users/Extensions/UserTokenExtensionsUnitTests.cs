using CustomCADs.Identity.Application.Contracts;
using CustomCADs.Identity.Application.Users.Dtos;
using CustomCADs.Identity.Application.Users.Extensions;
using CustomCADs.Identity.Domain.Users.Entities;

namespace CustomCADs.UnitTests.Identity.Application.Users.Extensions;

public class UserTokenExtensionsUnitTests : UsersBaseUnitTests
{
	private readonly Mock<IUserService> service = new();
	private readonly Mock<ITokenService> tokens = new();

	private static readonly TokenDto token = new("some-hashed-token", DateTimeOffset.UtcNow);
	private readonly User user = CreateUser();

	public UserTokenExtensionsUnitTests()
	{
		tokens.Setup(x => x.GenerateAccessToken(user.AccountId, user.Username, user.Role)).Returns(token);
		tokens.Setup(x => x.GenerateRefreshToken()).Returns(token.Value);
		tokens.Setup(x => x.GenerateCsrfToken()).Returns(token);
		service.Setup(x => x.AddRefreshTokenAsync(user, token.Value, true))
			.ReturnsAsync(RefreshToken.Create(token.Value, user.Id, true));
	}

	[Fact]
	public async Task IssueTokens_ShouldGenerateTokens()
	{
		// Arrange
		// Act
		await UserTokenExtensions.IssueTokens(service.Object, tokens.Object, user, longerSession: true);

		// Assert
		tokens.Verify(x => x.GenerateAccessToken(user.AccountId, user.Username, user.Role), Times.Once());
		tokens.Verify(x => x.GenerateRefreshToken(), Times.Once());
		tokens.Verify(x => x.GenerateCsrfToken(), Times.Once());
	}

	[Fact]
	public async Task IssueTokens_ShouldCallService()
	{
		// Arrange
		// Act
		await UserTokenExtensions.IssueTokens(service.Object, tokens.Object, user, longerSession: true);

		// Assert
		service.Verify(x => x.AddRefreshTokenAsync(user, token.Value, true));
	}

	[Fact]
	public async Task IssueTokens_ShouldReturnResult()
	{
		// Arrange
		// Act
		TokensDto result = await UserTokenExtensions.IssueTokens(service.Object, tokens.Object, user, longerSession: true);

		// Assert
		Assert.Multiple(
			() => Assert.Equal(token, result.AccessToken),
			() => Assert.Equal(token.Value, result.RefreshToken.Value), // has its own ExpiresAt generation logic
			() => Assert.Equal(token, result.CsrfToken)
		);
	}
}
