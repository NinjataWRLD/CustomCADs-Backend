namespace CustomCADs.Orders.Endpoints.Orders.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/client", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("06. Orders"));
        });
    }
}
