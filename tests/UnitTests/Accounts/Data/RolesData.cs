using CustomCADs.Accounts.Domain.Roles;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Accounts.Data;

using static Constants.Roles;
using static RoleConstants;

public static class RolesData
{
	public static readonly string ValidName = Customer;
	public static readonly string MinValidName = new('a', NameMinLength + 1);
	public static readonly string MaxValidName = new('a', NameMaxLength - 1);
	public const string InvalidName = "";
	public static readonly string MinInvalidName = new('a', NameMinLength - 1);
	public static readonly string MaxInvalidName = new('a', NameMaxLength + 1);

	public const string ValidDescription = CustomerDescription;
	public static readonly string MinValidDescription = new('a', DescriptionMinLength + 1);
	public static readonly string MaxValidDescription = new('a', DescriptionMaxLength - 1);
	public const string InvalidDescription = "";
	public static readonly string MinInvalidDescription = new('a', DescriptionMinLength - 1);
	public static readonly string MaxInvalidDescription = new('a', DescriptionMaxLength + 1);

	public static readonly RoleId ValidId = RoleId.New();
}
