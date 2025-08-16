namespace CustomCADs.Speedy.Core.Services.Models;

public record AccountModel(
	string Username,
	string Password,
	string? Language = null,
	long? ClientSystemId = null
);
