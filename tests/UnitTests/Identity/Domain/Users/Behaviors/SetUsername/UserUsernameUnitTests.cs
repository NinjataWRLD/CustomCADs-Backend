using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Behaviors.SetUsername;

using Data;

public class UserUsernameUnitTests : UsersBaseUnitTests
{
	[Theory]
	[ClassData(typeof(UserUsernameValidData))]
	public void SetUsername_ShouldNotThrowException(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);
	}

	[Theory]
	[ClassData(typeof(UserUsernameValidData))]
	public void SetUsername_PopulatesProperty(string username)
	{
		var user = CreateUser();

		user.SetUsername(username);

		Assert.Equal(user.Username, username);
	}

	[Theory]
	[ClassData(typeof(UserUsernameInvalidData))]
	public void SetUsername_ThrowsException_WhenUsernameIsInvalid(string username)
	{
		var user = CreateUser();

		Assert.Throws<CustomValidationException<User>>(
			() => user.SetUsername(username)
		);
	}
}
