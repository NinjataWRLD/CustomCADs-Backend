using CustomCADs.Identity.Domain.Users.ValueObjects;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using UserId = System.Guid;

namespace CustomCADs.Identity.Domain.Users;

public class User
{
    public User() : base() { }

    public User(string role, string username, Email email, AccountId accountId)
        : base()
    {
        Role = role;
        Username = username;
        Email = email;
        AccountId = accountId;
    }

    public UserId Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public Email Email { get; set; } = new();
    public string Role { get; set; } = string.Empty;
    public RefreshToken? RefreshToken { get; set; }
    public AccountId AccountId { get; set; }
}
