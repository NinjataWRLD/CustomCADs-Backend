namespace CustomCADs.Carts.Endpoints.ActiveCarts;

using static Constants.Roles;

public class ActiveCartsGroup : Group
{
    public ActiveCartsGroup()
    {
        Configure("carts/active", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("05. Active Cart"));
        });
    }
}
