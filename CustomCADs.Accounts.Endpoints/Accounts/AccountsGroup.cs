namespace CustomCADs.Accounts.Endpoints.Accounts;

using static Constants.Roles;

public class AccountsGroup : Group
{
    public AccountsGroup()
    {
        Configure("accounts", ep =>
        {
            ep.Roles(Admin);
            ep.Description(opt => opt.WithTags("09. Accounts Dashboard"));
        });
    }
}
