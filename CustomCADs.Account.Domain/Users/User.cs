using CustomCADs.Shared.Core.Bases.Entities;
using UserDto = (System.Guid Id, string Role, string Username, string Email);

namespace CustomCADs.Account.Domain.Users;

public class User : BaseAggregateRoot
{
    private User() { }
    private User(string role, string username, string email, string timeZone, string? firstName, string? lastName) : this()
    {
        RoleName = role;
        Username = username;
        Email = email;
        TimeZone = timeZone;
        FirstName = firstName;
        LastName = lastName;
    }

    public UserId Id { get; init; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string TimeZone { get; private set; } = string.Empty;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string RoleName { get; private set; } = string.Empty;

    public static User Create(
        string role,
        string username,
        string email,
        string timeZone,
        string? firstName = default,
        string? lastName = default
    ) => new User(role, username, email, timeZone, firstName, lastName)
            .ValidateRole()
            .ValidateUsername()
            .ValidateEmail()
            .ValidateFirstName()
            .ValidateLastName();

    public static IEnumerable<User> CreateRange(UserDto[] users)
        => users.Select(dto =>
            new User(dto.Role, dto.Username, dto.Email, "Sofia/Europe", null, null)
            {
                Id = new(dto.Id)
            }
            .ValidateRole()
            .ValidateUsername()
            .ValidateEmail()
            .ValidateFirstName()
            .ValidateLastName()
        );

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

    public User SetTimeZone(string timeZone)
    {
        TimeZone = timeZone;
        return this;
    }

    public User SetNames(string? firstName, string? lastName)
    {
        FirstName = firstName;
        LastName = lastName;
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
