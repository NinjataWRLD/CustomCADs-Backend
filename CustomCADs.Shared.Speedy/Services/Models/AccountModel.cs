namespace CustomCADs.Shared.Speedy.Services.Models;

public record AccountModel(
    string Username,
    string Password,
    string? Language,
    long? ClientSystemId
);
