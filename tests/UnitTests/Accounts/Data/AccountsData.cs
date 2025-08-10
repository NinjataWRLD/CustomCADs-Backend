using CustomCADs.Accounts.Domain.Accounts;
using CustomCADs.Shared.Domain;
using CustomCADs.Shared.Domain.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data;

using static AccountConstants;
using static Constants.Users;

public static class AccountsData
{
	public const string ValidUsername = CustomerUsername;
	public static readonly string MinValidUsername = new('a', NameMinLength + 1);
	public static readonly string MaxValidUsername = new('a', NameMaxLength - 1);
	public const string InvalidUsername = "";
	public static readonly string MinInvalidUsername = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidUsername = new('a', NameMaxLength + 1);

	public const string ValidEmail1 = CustomerEmail;
	public const string ValidEmail2 = ContributorEmail;
	public const string ValidEmail3 = DesignerEmail;
	public const string ValidEmail4 = AdminEmail;
	public const string InvalidEmail = "";
	public const string InvalidEmailLocal = "@domain.tld";
	public const string InvalidEmailDomain = "local@";
	public const string InvalidEmailTLD = "local@domain";
	public const string InvalidEmailTLDMin = "local@domain.a";

	public const string ValidPassword = "password123";
	public static readonly string MinInvalidPassword = new('a', PasswordMinLength - 1);

	public const string? ValidFirstName = "John";
	public const string? ValidFirstNameNull = null;
	public static readonly string MinInvalidFirstName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidFirstName = new('a', NameMaxLength + 1);

	public const string? ValidLastName = "Doe";
	public const string? ValidLastNameNull = null;
	public static readonly string MinInvalidLastName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidLastName = new('a', NameMaxLength + 1);

	public static readonly AccountId ValidId = AccountId.New(CustomerAccountId);
}
