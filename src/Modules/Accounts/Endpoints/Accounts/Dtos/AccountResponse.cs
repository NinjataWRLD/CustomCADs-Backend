namespace CustomCADs.Accounts.Endpoints.Accounts.Dtos;

public sealed record AccountResponse(
	string Username,
	string Email,
	string Role,
	string? FirstName,
	string? LastName
);
