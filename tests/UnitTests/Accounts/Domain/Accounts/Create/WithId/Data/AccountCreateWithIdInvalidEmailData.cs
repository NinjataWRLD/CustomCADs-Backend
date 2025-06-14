namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Create.WithId.Data;

using static AccountsData;

public class AccountCreateWithIdInvalidEmailData : AccountCreateWithIdData
{
	public AccountCreateWithIdInvalidEmailData()
	{
		Add(RolesData.ValidName, ValidUsername, InvalidEmail, ValidFirstName, ValidLastName);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailLocal, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MaxValidName, MaxValidUsername, InvalidEmailDomain, ValidFirstName, ValidLastName);
		Add(RolesData.ValidName, ValidUsername, InvalidEmailTLD, ValidFirstNameNull, ValidLastNameNull);
		Add(RolesData.MinValidName, MinValidUsername, InvalidEmailTLDMin, ValidFirstNameNull, ValidLastNameNull);
	}
}
