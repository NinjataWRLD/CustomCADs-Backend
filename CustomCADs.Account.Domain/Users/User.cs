using CustomCADs.Account.Domain.Roles;
using CustomCADs.Account.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Domain;

namespace CustomCADs.Account.Domain.Users;

public class User : IAggregateRoot
{
    private User() { }
    private User(string role, string username, string email, string? firstName, string? lastName) : this()
    {
        RoleName = role;
        Username = username;
        Email = email;
        NameInfo = NameInfo.Create(firstName, lastName);
    }

    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public NameInfo NameInfo { get; set; } = NameInfo.Create();
    public string RoleName { get; set; } = string.Empty;
    public Role Role { get; set; } = null!;

    public static User Create(
        string role,
        string username,
        string email,
        string? firstName = default,
        string? lastName = default)
    {
        return new(role, username, email, firstName, lastName);
    }
}
