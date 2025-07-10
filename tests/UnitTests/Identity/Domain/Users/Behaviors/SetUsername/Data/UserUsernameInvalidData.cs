namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername.Data;

using static UsersData;

public class UserUsernameInvalidData : UserUsernameData
{
	public UserUsernameInvalidData()
	{
		Add(InvalidUsername);
		Add(MinInvalidUsername);
		Add(MaxInvalidUsername);
	}
}
