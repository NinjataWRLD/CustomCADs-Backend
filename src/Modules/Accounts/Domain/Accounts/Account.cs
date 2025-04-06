using CustomCADs.Shared.Core.Bases.Entities;
using CustomCADs.Shared.Core.Common.TypedIds.Catalog;

namespace CustomCADs.Accounts.Domain.Accounts;

public class Account : BaseAggregateRoot
{
    private readonly List<ProductId> viewedProductIds = [];

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
    public IReadOnlyCollection<ProductId> ViewedProductIds => viewedProductIds.AsReadOnly();

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
            .ValidateTimeZone()
            .ValidateFirstName()
            .ValidateLastName();

    public static Account CreateWithId(
        AccountId id,
        string role,
        string username,
        string email,
        string timeZone,
        string? firstName = default,
        string? lastName = default
    ) => new Account(role, username, email, timeZone, firstName, lastName)
    {
        Id = id,
    }
    .ValidateRole()
    .ValidateUsername()
    .ValidateEmail()
    .ValidateTimeZone()
    .ValidateFirstName()
    .ValidateLastName();

    public Account AddViewedProduct(ProductId id)
    {
        if (!viewedProductIds.Contains(id))
        {
            viewedProductIds.Add(id);
        }

        return this;
    }

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
}
