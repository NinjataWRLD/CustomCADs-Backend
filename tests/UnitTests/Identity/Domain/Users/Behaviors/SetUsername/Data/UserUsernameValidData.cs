namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername.Data;

using static UsersData;

public class UserUsernameValidData : UserUsernameData
{
	public UserUsernameValidData()
	{
		Add(MinValidUsername);
		Add(MaxValidUsername);
	}
}
