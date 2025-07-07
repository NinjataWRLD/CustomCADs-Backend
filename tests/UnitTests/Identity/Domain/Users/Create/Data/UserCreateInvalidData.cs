using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.Data;

using static UsersData;

public class UserCreateInvalidData : TheoryData<string, string, string>
{
	public UserCreateInvalidData()
	{
		// Role
		Add(InvalidRole, MaxValidUsername, ValidEmail);

		// Username
		Add(ValidRole, InvalidUsername, ValidEmail);
		Add(ValidRole, MaxInvalidUsername, ValidEmail);
		Add(ValidRole, MinInvalidUsername, ValidEmail);

		// Email
		Add(ValidRole, MaxValidUsername, InvalidEmail);
	}
}
