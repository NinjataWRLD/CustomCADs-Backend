namespace CustomCADs.Accounts.Endpoints.Roles.Endpoints;

using static Constants.Roles;

public class RolesGroup : Group
{
	public RolesGroup()
	{
		Configure("roles", ep =>
		{
			ep.Roles(Admin);
			ep.Description(opt => opt.WithTags("14. Roles Dashboard"));
		});
	}
}
