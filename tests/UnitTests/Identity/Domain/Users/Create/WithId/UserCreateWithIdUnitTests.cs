using CustomCADs.Identity.Domain.Users;
using CustomCADs.Shared.Core.Common.Exceptions.Domain;

namespace CustomCADs.UnitTests.Identity.Domain.Users.Create.WithId;

using static UsersData;

public class UserCreateWithIdUnitTests : UsersBaseUnitTests
{
	[Fact]
	public void CreateWithId_ShouldNotThrowException()
	{
		CreateUserWithId(ValidId, ValidRole, MaxValidUsername, ValidEmail, ValidAccountId);
	}

	[Fact]
	public void CreateWithId_ShouldPopulateProperties()
	{
		User user = CreateUserWithId(ValidId, ValidRole, MaxValidUsername, ValidEmail, ValidAccountId);

		Assert.Multiple(
			() => Assert.Equal(ValidRole, user.Role),
			() => Assert.Equal(MaxValidUsername, user.Username),
			() => Assert.Equal(ValidEmail, user.Email.Value),
			() => Assert.Equal(ValidAccountId, user.AccountId)
		);
	}

	[Theory]
	[ClassData(typeof(Data.UserCreateInvalidData))]
	public void CreateWithId_ShouldThrowException_WhenUserInvalid(string role, string username, string email)
	{
		Assert.Throws<CustomValidationException<User>>(
			() => CreateUserWithId(ValidId, role, username, email, ValidAccountId)
		);
	}
}
