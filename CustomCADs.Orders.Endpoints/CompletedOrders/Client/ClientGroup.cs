namespace CustomCADs.Orders.Endpoints.CompletedOrders.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/client/completed", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("09. Completed Orders"));
        });
    }
}
