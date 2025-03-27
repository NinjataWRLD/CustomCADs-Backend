namespace CustomCADs.Carts.Endpoints.ActiveCarts.Endpoints;

using static Constants.Roles;

public class ActiveCartsGroup : Group
{
    public ActiveCartsGroup()
    {
        Configure("carts/active", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("03. Active Cart"));
        });
    }
}
