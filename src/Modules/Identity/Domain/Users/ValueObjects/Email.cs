namespace CustomCADs.Identity.Domain.Users.ValueObjects;

public record Email(
	string Value = "",
	bool IsVerified = false
);
