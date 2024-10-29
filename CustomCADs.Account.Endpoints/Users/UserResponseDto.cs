namespace CustomCADs.Account.Endpoints.Users;

public record UserResponseDto(
    string Username,
    string Email,
    string Role,
    string? FirstName,
    string? LastName
);
