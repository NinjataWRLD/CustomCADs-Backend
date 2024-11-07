using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Users.ValueObjects;

namespace CustomCADs.Account.Domain.Users;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public NameInfo NameInfo { get; set; } = new();
    public string RoleName { get; set; } = string.Empty;
    public Role Role { get; set; } = new();
}
