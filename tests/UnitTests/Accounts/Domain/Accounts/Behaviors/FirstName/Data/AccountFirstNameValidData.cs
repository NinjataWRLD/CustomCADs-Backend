﻿namespace CustomCADs.UnitTests.Accounts.Domain.Accounts.Behaviors.FirstName.Data;

using static AccountsData;

public class AccountFirstNameValidData : AccountFirstNameData
{
	public AccountFirstNameValidData()
	{
		Add(ValidFirstName);
		Add(ValidFirstNameNull);
	}
}
