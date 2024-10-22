using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Users.ValueObjects;

namespace CustomCADs.Account.Domain.Users;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public NameInfo NameInfo { get; set; } = new();
    public RefreshToken RefreshToken { get; set; } = new();
    public required string RoleName { get; set; }
    public required Role Role { get; set; }
}
