namespace CustomCADs.Speedy.Core.Models;

public record PhoneNumberModel(
	string Number,
	string? Extension = default
);
