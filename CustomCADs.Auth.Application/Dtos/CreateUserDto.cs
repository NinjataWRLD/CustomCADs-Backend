namespace CustomCADs.Auth.Application.Dtos;

public record CreateUserDto(
    string Role,
    string Username,
    string Email,
    string Password,
    string? FirstName = default,
    string? LastName = default
);
