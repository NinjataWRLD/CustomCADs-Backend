namespace CustomCADs.Identity.Application.Users.Dtos;

public record TokensDto(
	TokenDto AccessToken,
	TokenDto RefreshToken,
	TokenDto CsrfToken,
	string Role
);
