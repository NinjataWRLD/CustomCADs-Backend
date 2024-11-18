namespace CustomCADs.Account.Endpoints.Users;

using static Constants.Roles;

public class UsersGroup : Group
{
    public UsersGroup()
    {
        Configure("users", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt => opt.WithTags("Users"));
        });
    }
}
