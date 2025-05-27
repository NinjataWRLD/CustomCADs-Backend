namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints.Post;

public sealed record PostAccountRequest(
	string Role,
	string Username,
	string Email,
	string Password,
	string? FirstName = default,
	string? LastName = default
);
