using CustomCADs.Account.Domain.Users.ValueObjects;

namespace CustomCADs.Account.Application.Users.Common.Dtos;

public class CreateUserDto
{
    public required string RoleName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required NameInfo NameInfo { get; set; }
}