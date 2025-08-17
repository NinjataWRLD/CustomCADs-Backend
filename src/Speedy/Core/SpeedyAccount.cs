namespace CustomCADs.Speedy.Core;

public record SpeedyAccount(
	string Username,
	string Password,
	string? Language = null,
	long? ClientSystemId = null
);
