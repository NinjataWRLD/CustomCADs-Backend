namespace CustomCADs.UnitTests.Accounts.Application.Accounts;

public class AccountsBaseUnitTests
{
    protected const string Username = "J0hn_D03";
    protected const string Email = "john@doe.com";
    protected const string TimeZone = "Continent/Capital";
    protected const string Password = "password123";
    protected const string? FirstName = "John";
    protected const string? LastName = "Doe";
    protected static readonly CancellationToken ct = CancellationToken.None;

    protected static Account CreateAccount(string role, string username = Username, string email = Email, string timeZone = TimeZone, string? firstName = null, string? lastName = null)
        => Account.Create(role, username, email, timeZone, firstName, lastName);
}
