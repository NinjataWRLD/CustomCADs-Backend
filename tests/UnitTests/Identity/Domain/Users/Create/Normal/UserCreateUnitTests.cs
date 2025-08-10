using CustomCADs.Shared.Domain.Exceptions;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.Normal;

using static UsersData;

public class UserCreateUnitTests : UsersBaseUnitTests
{
	[Fact]
	public void Create_ShouldNotThrowException()
	{
		CreateUser(ValidRole, MaxValidUsername, ValidEmail, ValidAccountId);
	}

	[Fact]
	public void Create_ShouldPopulateProperties()
	{
		User user = CreateUser(ValidRole, MaxValidUsername, ValidEmail, ValidAccountId);

		Assert.Multiple(
			() => Assert.Equal(ValidRole, user.Role),
			() => Assert.Equal(MaxValidUsername, user.Username),
			() => Assert.Equal(ValidEmail, user.Email.Value),
			() => Assert.Equal(ValidAccountId, user.AccountId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.UserCreateInvalidData))]
	public void Create_ShouldThrowException_WhenUserInvalid(string role, string username, string email)
	{
		Assert.Throws<CustomValidationException<User>>(
			() => CreateUser(role, username, email, ValidAccountId)
		);
	}
}
