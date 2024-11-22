namespace CustomCADs.Shared.Speedy.Models;

public record AccountModel(
    string Username, 
    string Password,
    string? Language,
    long? ClientSystemId
);
