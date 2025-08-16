namespace CustomCADs.Speedy.Core.Services.Models;

public record PhoneNumberModel(
	string Number,
	string? Extension = default
);
