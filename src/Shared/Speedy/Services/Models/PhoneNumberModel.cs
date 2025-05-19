namespace CustomCADs.Shared.Speedy.Services.Models;

public record PhoneNumberModel(
	string Number,
	string? Extension = default
);
