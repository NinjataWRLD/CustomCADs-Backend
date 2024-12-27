using CustomCADs.Accounts.Domain.Accounts.Validation;
using CustomCADs.Shared.Core.Bases.Entities;
using UserDto = (System.Guid Id, string Role, string Username, string Email);

namespace CustomCADs.Accounts.Domain.Accounts;

public class Account : BaseAggregateRoot
{
    private Account() { }
    private Account(string role, string username, string email, string timeZone, string? firstName, string? lastName) : this()
    {
        RoleName = role;
        Username = username;
        Email = email;
        TimeZone = timeZone;
        FirstName = firstName;
        LastName = lastName;
    }

    public AccountId Id { get; init; }
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string TimeZone { get; private set; } = string.Empty;
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string RoleName { get; private set; } = string.Empty;

    public static Account Create(
        string role,
        string username,
        string email,
        string timeZone,
        string? firstName = default,
        string? lastName = default
    ) => new Account(role, username, email, timeZone, firstName, lastName)
            .ValidateRole()
            .ValidateUsername()
            .ValidateEmail()
            .ValidateFirstName()
            .ValidateLastName();

    public static IEnumerable<Account> CreateRange(UserDto[] users)
        => users.Select(dto =>
            new Account(dto.Role, dto.Username, dto.Email, "Europe/Sofia", null, null)
            {
                Id = new(dto.Id)
            }
            .ValidateRole()
            .ValidateUsername()
            .ValidateEmail()
            .ValidateFirstName()
            .ValidateLastName()
        );

    public Account SetUsername(string username)
    {
        Username = username;
        this.ValidateUsername();
        return this;
    }

    public Account SetEmail(string email)
    {
        Email = email;
        this.ValidateEmail();
        return this;
    }

    public Account SetTimeZone(string timeZone)
    {
        TimeZone = timeZone;
        return this;
    }

    public Account SetFirstName(string? firstName)
    {
        FirstName = firstName;
        this.ValidateFirstName();
        return this;
    }
    
    public Account SetLastName(string? lastName)
    {
        LastName = lastName;
        this.ValidateLastName();
        return this;
    }

    public Account SetRole(string role)
    {
        RoleName = role;
        this.ValidateRole();
        return this;
    }
}
