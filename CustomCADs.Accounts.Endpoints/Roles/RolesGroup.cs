namespace CustomCADs.Accounts.Endpoints.Roles;

using static Constants.Roles;

public class RolesGroup : Group
{
    public RolesGroup()
    {
        Configure("roles", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt => opt.WithTags("12. Roles Dashboard"));
        });
    }
}
