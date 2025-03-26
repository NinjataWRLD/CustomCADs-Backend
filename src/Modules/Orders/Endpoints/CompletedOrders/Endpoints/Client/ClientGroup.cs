namespace CustomCADs.Orders.Endpoints.CompletedOrders.Endpoints.Client;

using static Constants.Roles;

public class ClientGroup : Group
{
    public ClientGroup()
    {
        Configure("orders/completed/client", ep =>
        {
            ep.Roles(Client);
            ep.Description(d => d.WithTags("08. Completed Orders"));
        });
    }
}
