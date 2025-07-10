using CustomCADs.Identity.Domain.Users;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;
using CustomCADs.Shared.Core.Common.TypedIds.Identity;

namespace CustomCADs.UnitTests.Identity.Data;

using static UserConstants;
using static Constants;

public static class UsersData
{
	public const string ValidRole = Roles.Customer;
	public static readonly string InvalidRole = string.Empty;

	public static readonly string MinValidUsername = new('a', UsernameMinLength + 1);
	public static readonly string MaxValidUsername = new('a', UsernameMaxLength - 1);
	public static readonly string InvalidUsername = string.Empty;
	public static readonly string MinInvalidUsername = new('a', UsernameMinLength - 1);
	public static readonly string MaxInvalidUsername = new('a', UsernameMaxLength + 1);

	public static readonly string ValidEmail = Users.CustomerEmail;
	public static readonly string InvalidEmail = string.Empty;

	public static readonly string MinValidPassword = new('a', PasswordMinLength + 1);
	public static readonly string InvalidPassword = string.Empty;
	public static readonly string MinInvalidPassword = new('a', PasswordMinLength - 1);

	public static readonly UserId ValidId = UserId.New();
	public static readonly AccountId ValidAccountId = AccountId.New();
}
