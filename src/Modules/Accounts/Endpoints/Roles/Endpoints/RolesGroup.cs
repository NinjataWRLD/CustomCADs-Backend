namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints;

using static Constants.Roles;
using static EndpointsConstants;

public class RolesGroup : Group
{
	public RolesGroup()
	{
		Configure(Paths.Roles, ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags(Tags[Paths.Roles]));
		});
	}
}
