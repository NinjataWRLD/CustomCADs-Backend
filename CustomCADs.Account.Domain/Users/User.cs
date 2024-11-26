using CustomCADs.Account.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Bases.Entities;

namespace CustomCADs.Account.Domain.Users;

public class User : BaseAggregateRoot
{
    private User() { }
    private User(string role, string username, string email, string? firstName, string? lastName) : this()
    {
        RoleName = role;
        Username = username;
        Email = email;
        Names = Names.Create(firstName, lastName);
    }

    public UserId Id { get; init; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public Names Names { get; private set; } = Names.Create();
    public string RoleName { get; private set; } = string.Empty;

    public static User Create(
        string role,
        string username,
        string email,
        string? firstName = default,
        string? lastName = default
    ) => new User(role, username, email, firstName, lastName)
            .ValidateRole()
            .ValidateUsername()
            .ValidateEmail()
            .ValidateFirstName()
            .ValidateLastName();

    public User SetUsername(string username)
    {
        Username = username;
        this.ValidateUsername();
        return this;
    }

    public User SetEmail(string email)
    {
        Email = email;
        this.ValidateEmail();
        return this;
    }

    public User SetNames(string? firstName, string? lastName)
    {
        Names = Names.Create(firstName, lastName);
        this.ValidateFirstName();
        this.ValidateLastName();
        return this;
    }

    public User SetRole(string role)
    {
        RoleName = role;
        this.ValidateRole();
        return this;
    }
}
