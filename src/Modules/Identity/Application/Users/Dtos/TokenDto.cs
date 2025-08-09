namespace CustomCADs.Identity.Application.Users.Dtos;

public record TokenDto(string Value, DateTimeOffset ExpiresAt);
