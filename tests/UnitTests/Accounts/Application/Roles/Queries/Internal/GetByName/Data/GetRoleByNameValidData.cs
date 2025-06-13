namespace CustomCADs.UnitTests.Accounts.Application.Roles.Queries.Internal.GetByName.Data;

using static RolesData;

public class GetRoleByNameValidData : GetRoleByNameData
{
	public GetRoleByNameValidData()
	{
		Add(ValidName);
		Add(MinValidName);
		Add(MaxValidName);
	}
}
