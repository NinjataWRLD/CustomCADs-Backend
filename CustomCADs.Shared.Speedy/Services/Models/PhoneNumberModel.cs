namespace CustomCADs.Shared.Speedy.Models;

public record PhoneNumberModel(
    string Number,
    string? Extension = default
);