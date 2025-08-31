namespace CustomCADs.Accounts.Endpoints.Accounts.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class AccountsGroup : Group
{
	public AccountsGroup()
	{
		Configure(Paths.Accounts, ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags(Tags[Paths.Accounts]));
		});
	}
}
